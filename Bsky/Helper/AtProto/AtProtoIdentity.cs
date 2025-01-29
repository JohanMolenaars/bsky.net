using Bsky.Net.Model;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Bsky.Net.Helper.AtProto
{
    internal class Identity
    {
        private readonly HttpClient _client;

        internal Identity(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Resolves a handle to a DID (Decentralized Identifier).
        /// </summary>
        /// <param name="handle">The handle to resolve.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="Result{Did}"/> object.</returns>
        internal async Task<Result<Did>> ResolveHandle(string handle, CancellationToken cancellationToken)
        {
            string url = $"{Constants.Urls.AtProtoIdentity.ResolveHandle}?handle={handle}";
            Result<HandleResolution?> result = await _client.Get<HandleResolution>(url, cancellationToken);
            return result.Match(resolution =>
            {
                Result<Did> did = resolution!.Did!;
                return did;
            }, error => error!);
        }
    }
}
