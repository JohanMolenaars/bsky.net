using Bsky.Net.Model;
using System.Text.Json.Serialization;

namespace Bsky.Net.Helper.Commands.Bsky.Feed
{
    /// <summary>
    /// Represents the response received after creating a post.
    /// </summary>
    public class CreatePostResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePostResponse"/> class.
        /// </summary>
        /// <param name="uri">The URI of the created post.</param>
        /// <param name="cid">The CID of the created post.</param>
        [JsonConstructor]
        public CreatePostResponse(AtUri uri, string cid)
        {
            Cid = cid;
            Uri = uri;
        }

        /// <summary>
        /// Gets the CID of the created post.
        /// </summary>
        public string Cid { get; }

        /// <summary>
        /// Gets the URI of the created post.
        /// </summary>
        public AtUri Uri { get; }
    }
}
