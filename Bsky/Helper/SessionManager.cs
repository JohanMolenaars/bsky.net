using Bsky.Net.Helper.AtProto;
using Bsky.Net.Helper.Multiples;
using Bsky.Net.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Bsky.Net.Helper
{
    internal class SessionManager : IDisposable
    {
        private static readonly JwtSecurityTokenHandler JwtSecurityTokenHandler = new();

        private static readonly TokenValidationParameters DefaultTokenValidationParameters = new()
        {
            ValidateActor = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuer = false,
            ValidateSignatureLast = false,
            ValidateTokenReplay = false,
            ValidateIssuerSigningKey = false,
            ValidateWithLKG = false,
            LogValidationExceptions = false
        };

        private readonly BlueskyApiOptions _options;
        private System.Timers.Timer? _timer;
        private int _refreshing;
        private readonly Server _server;
        private bool _disposed;

        internal Session? Session { get; private set; }

        internal SessionManager(BlueskyApiOptions options, Server server, Session session)
        {
            _options = options;
            _server = server;
            _server.TokenRefreshed += OnTokenRefreshed;
            Session = session;
        }

        private void OnTokenRefreshed(Session session)
        {
            SetSession(session);
        }

        internal void SetSession(Session session)
        {
            Session = session;
            if (!_options.AutoRenewSession)
            {
                return;
            }

            _timer ??= new Timer();

            ConfigureRefreshTokenTimer();
        }


        /// <summary>
        /// Configures the timer to refresh the token at the appropriate interval.
        /// </summary>
        /// <remarks>
        /// This method initializes and starts a timer that triggers the token refresh process.
        /// The interval for the timer is determined by the time remaining until the next token renewal.
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the timer or session is null.
        /// </exception>
        private void ConfigureRefreshTokenTimer()
        {
            Timer timer = _timer.ThrowIfNull();
            TimeSpan timeToNextRenewal = GetTimeToNextRenewal(Session.ThrowIfNull());
            timer.Elapsed += this.RefreshToken;
            timer.Interval = timeToNextRenewal.TotalSeconds;
            timer.Enabled = true;
            timer.Start();
        }

        /// <summary>
        /// Refreshes the session token asynchronously when triggered by a timer event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private async void RefreshToken(object? sender, ElapsedEventArgs e)
        {
            if (Interlocked.Increment(ref _refreshing) > 1)
            {
                Interlocked.Decrement(ref _refreshing);
                return;
            }

            _timer.ThrowIfNull().Enabled = false;
            try
            {
                Multiple<Session, Error> result =
                    await _server.ThrowIfNull().RefreshSession(Session.ThrowIfNull(), CancellationToken.None);

                result
                    .Switch(s =>
                    {
                        if (!_options.TrackSession)
                        {
                            return;
                        }

                        SetSession(s);
                    }, _ => { });
            }
            finally
            {
                Interlocked.Decrement(ref _refreshing);
            }
        }


        /// <summary>
        /// Calculates the time remaining until the next session renewal is required.
        /// </summary>
        /// <param name="session">The current session containing the refresh JWT.</param>
        /// <returns>A <see cref="TimeSpan"/> representing the time remaining until the next renewal.</returns>
        private static TimeSpan GetTimeToNextRenewal(Session session)
        {
            JwtSecurityTokenHandler
                .ValidateToken(
                    session.RefreshJwt,
                    DefaultTokenValidationParameters,
                    out SecurityToken token);
            return token.ValidTo.ToUniversalTime() - DateTime.UtcNow;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed || _timer is null)
            {
                return;
            }

            if (disposing)
            {
                _timer.Enabled = false;
                _timer.Dispose();
                _timer = null;
            }

            _disposed = true;
        }

        public void Dispose() => Dispose(true);
    }
}
