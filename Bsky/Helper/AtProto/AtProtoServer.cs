using Bsky.Net.Helper.AtProto.Commands.Server;
using Bsky.Net.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Bsky.Net.Helper.AtProto
{
    internal delegate void UserLoggedIn(Session s);

    internal delegate void TokenRefreshed(Session s);

    internal class Server
    {
        private readonly BlueskyApiOptions _options;
        private readonly IHttpClientFactory _factory;

        internal Server(IHttpClientFactory factory, BlueskyApiOptions options)
        {
            _factory = factory;
            _options = options;
        }

        /// <summary>
        /// Logs in a user using the provided login command and returns a session result.
        /// </summary>
        /// <param name="command">The login command containing the user's credentials.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Session}"/> object.</returns>
        /// <remarks>
        /// If the <see cref="_options.TrackSession"/> is set to true, the <see cref="UserLoggedIn"/> event is invoked upon successful login.
        /// </remarks>
        internal async Task<Result<Session>> Login(Login command, CancellationToken cancellationToken)
        {
            using HttpClient client = _factory.CreateClient(Constants.BlueskyApiClient);
            Result<Session> result =
                await client.Post<Login, Session>(Constants.Urls.AtProtoServer.Login, command, cancellationToken);
            return
                result
                    .Match(s =>
                    {
                        if (!_options.TrackSession)
                        {
                            return result;
                        }

                        UserLoggedIn?.Invoke(s);

                        return result;
                    }, error => error!);
        }

        /// <summary>
        /// Refreshes the session using the provided session object and updates the session token if tracking is enabled.
        /// </summary>
        /// <param name="session">The current session object containing the refresh token.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Session}"/> object which holds the refreshed session or an error.</returns>
        /// <remarks>
        /// If the <c>TrackSession</c> option is enabled, the <see cref="TokenRefreshed"/> event is invoked with the new session.
        /// </remarks>
        public async Task<Result<Session>> RefreshSession(
            Session session,
            CancellationToken cancellationToken)
        {
            using HttpClient client = _factory.CreateClient(Constants.BlueskyApiClient);
            client
                .DefaultRequestHeaders
                .Authorization = new AuthenticationHeaderValue("Bearer", session.RefreshJwt);

            var result = await client.Post<Session>(Constants.Urls.AtProtoServer.RefreshSession, cancellationToken);
            return
                result
                    .Match(s =>
                    {
                        if (!_options.TrackSession)
                        {
                            return result;
                        }

                        TokenRefreshed?.Invoke(s);
                        return result;
                    }, error => error!);
        }

        internal UserLoggedIn? UserLoggedIn { get; set; }

        internal TokenRefreshed? TokenRefreshed { get; set; }
    }
}
