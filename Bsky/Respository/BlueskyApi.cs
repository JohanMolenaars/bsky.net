using Bsky.Net.Helper;
using Bsky.Net.Helper.Strings;
using Bsky.Net.Model;
using Bsky.Net.Helper.AtProto;
using Bsky.Net.Helper.AtProto.Commands.Server;
using Bsky.Net.Helper.BlueSky;
using Bsky.Net.Helper.Commands.Bsky.Feed;
using Bsky.Net.Model.Queries;
using Bsky.Net.Model.Queries.Feed;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using static Bsky.Net.Helper.Constants;
using static Bsky.Net.Model.Following;

namespace Bsky.Net.Respository
{
    internal class BlueskyApi : IBlueskyApi, IDisposable
    {
        private readonly BlueskyApiOptions _options;
        private readonly Server _server;
        private readonly HttpClient _client;
        private readonly Identity _identity;
        private readonly Actor _actor;
        private readonly Repo _repo;

        private SessionManager? _sessionManager;

        public BlueskyApi(IHttpClientFactory factory, BlueskyApiOptions options)
        {
            _options = options;
            _client = factory.CreateClient(Constants.BlueskyApiClient);
            _server = new(factory, _options);
            _identity = new(_client);
            _repo = new(_client);
            _actor = new(_client);
            _server.UserLoggedIn += OnUserLoggedIn;
            _server.TokenRefreshed += UpdateBearerToken;
        }

        public Task<Result<Session>> Login(Login command, CancellationToken cancellationToken)
        {
            return _server.Login(command, cancellationToken);
        }

        public Task<Result<Session>> RefreshSession(
            Session session,
            CancellationToken cancellationToken) => _server.RefreshSession(session, cancellationToken);

        /// <summary>
        /// Get the user profile
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<Profile>> GetProfile(Did did, CancellationToken cancellationToken)
            => _actor.GetProfile(did, cancellationToken)!;

        public Task<Result<Profile>> GetProfile(CancellationToken cancellationToken)
            => GetProfile(_sessionManager.ThrowIfNull().Session.ThrowIfNull().Did, cancellationToken);

        /// <summary>
        /// Get the user profile
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<List<Profile>>> GetFollowers(Did did, CancellationToken cancellationToken)
            => _actor.GetFollowers(did, cancellationToken)!;

        public Task<Result<List<Profile>>> GetFollowers(CancellationToken cancellationToken)
             => GetFollowers(_sessionManager.ThrowIfNull().Session.ThrowIfNull().Did, cancellationToken);


        /// <summary>
        /// Get Following
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<List<Follow>>> GetFollowing(Did did, CancellationToken cancellationToken)
            => _actor.GetFollowing(did, cancellationToken)!;

        public Task<Result<List<Follow>>> GetFollowing(CancellationToken cancellationToken)
             => GetFollowing(_sessionManager.ThrowIfNull().Session.ThrowIfNull().Did, cancellationToken);



        /// <summary>
        /// Get All Follow Records
        /// </summary>
        /// <param name="did"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<List<Record>>> GetAllFollowRecords(string collection, Did did, CancellationToken cancellationToken)
            => _repo.GetAllFollowRecords(collection,did, cancellationToken)!;

        public Task<Result<List<Record>>> GetAllFollowRecords(string collection, CancellationToken cancellationToken)
             => GetAllFollowRecords(collection,_sessionManager.ThrowIfNull().Session.ThrowIfNull().Did, cancellationToken);


        public Task<Result<Did>> ResolveHandle(
            string handle,
            CancellationToken cancellationToken) => _identity.ResolveHandle(handle, cancellationToken);

        public Task<Result<CreatePostResponse>> CreatePost(
            CreatePost command, 
            CancellationToken cancellationToken)
        {


            if (command.images != null && command.images.Length > 4)
            { 
                throw new Exception("Maximum of 4 images allowed");
            }
            UploadBlobResponse? x;
            Error? y;
            UnicodeString utf16 = new UnicodeString(command.Text);
            var facts = Facets.DetectFacets(utf16);

            Embed embed = new Embed();
            embed.Images = new List<Image>();
            embed.Type = Types.Bluesky.Embed.Images;
            if (command.images != null && command.images.Length > 0)
            {
                Result<UploadBlobResponse> imageResponse;
                Image img;

                foreach (var image in command.images)
                {

                    imageResponse = _repo.Upload(image.Item1, cancellationToken).Result;
                    imageResponse.TryPickT0(out x, out y);

                    img = new Image
                    {
                        alt = image.Item1 != null ? image.Item2 : "Image",
                        image = x.blob,
                        aspectRatio = new AspectRatio { width = 640, height = 480 }
                    };
                    embed.Images.Add(img);
                }

            }

             CreateRecord record = new(
               Types.Bluesky.Feed.Post,
                _sessionManager!.Session!.Did.ToString()!,
                new RecordPost()
                {
                    Text = command.Text,
                    Type = Types.Bluesky.Feed.Post,
                    CreatedAt = DateTime.UtcNow,
                    Facets = facts,
                    Embed = embed

                });

            return _repo.Create(record, cancellationToken);
        }

        public Task<Result<AuthorFeed>> Query(GetAuthorFeed query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _sessionManager?.Dispose();
        }

        private void UpdateBearerToken(Session session)
        {
            _client
                    .DefaultRequestHeaders
                    .Authorization =
                new AuthenticationHeaderValue("Bearer", session.AccessJwt);
        }

        private void OnUserLoggedIn(Session session)
        {
            UpdateBearerToken(session);

            if (!_options.TrackSession)
            {
                return;
            }

            if (_sessionManager is null)
            {
                _sessionManager = new SessionManager(_options, _server, session);
            }
            else
            {
                _sessionManager.SetSession(session);
            }
        }
    }
}
