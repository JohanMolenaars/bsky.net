using Bsky.Net.Model;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using static Bsky.Net.Helper.Constants;
using static Bsky.Net.Model.Following;

namespace Bsky.Net.Helper.BlueSky
{
    internal class Actor
    {
        private readonly HttpClient _client;

        public Actor(HttpClient client)
        {
            _client = client;
        }


        /// <summary>
        /// Retrieves the profile information for a given DID (Decentralized Identifier).
        /// </summary>
        /// <param name="did">The DID of the actor whose profile is to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Profile}"/> object which includes the profile information if successful, or null if not.</returns>
        public Task<Result<Profile?>> GetProfile(Did did, CancellationToken cancellationToken)
        {
            string url = $"{Constants.Urls.Bluesky.GetActorProfile}?actor={did}";
            return _client.Get<Profile>(url, cancellationToken);
        }


        /// <summary>
        /// Retrieves the list of followers for a given actor (Did).
        /// </summary>
        /// <param name="did">The decentralized identifier (Did) of the actor whose followers are to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{T}"/> object with a list of <see cref="Profile"/> objects representing the followers, or null if no followers are found.</returns>
        public Task<Result<List<Profile>?>> GetFollowers(Did did, CancellationToken cancellationToken)
        {
            List<Profile>? followers = new();
            string cursor = null;
            do
            {
                string url = $"{Urls.Bluesky.GetFollowers}?actor={did}&limit=100";
                url += (cursor != null ? $"&cursor={cursor}" : "");

                var response = _client.Get<FollowersResponse>(url, cancellationToken).Result;
                response.Switch(response =>
                {
                    followers.AddRange(response.Followers);
                    cursor = response.Cursor;
                }, _ => { });

            } while (!string.IsNullOrEmpty(cursor));

            return Task.FromResult( new Result<List<Profile>?>(followers));
        }

        /// <summary>
        /// Retrieves a list of followers for a given actor (Did).
        /// </summary>
        /// <param name="did">The unique identifier of the actor whose followers are to be retrieved.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a Result object with a list of followers or null.</returns>
        public Task<Result<List<Follow>?>> GetFollowing(Did did, CancellationToken cancellationToken)
        {
            List<Follow>? followers = new();
            string cursor = null;
            do
            {
                string url = $"{Urls.Bluesky.GetFollowing}?actor={did}&limit=100";
                url += (cursor != null ? $"&cursor={cursor}" : "");

                var response = _client.Get<Following>(url, cancellationToken).Result;
                response.Switch(response =>
                {
                    followers.AddRange(response.Follows);
                    cursor = response.Cursor;
                }, _ => { });

            } while (!string.IsNullOrEmpty(cursor));

            return Task.FromResult(new Result<List<Follow>?>(followers));
        }
    }
}
