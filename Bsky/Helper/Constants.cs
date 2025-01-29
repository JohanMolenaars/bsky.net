namespace Bsky.Net.Helper
{
    internal static class Constants
    {
        internal const string BlueskyApiClient = "Bluesky";
        internal const string ContentMediaType = "application/json";
        internal const string AcceptedMediaType = "application/json";

        internal static class Urls
        {
            internal static class AtProtoServer
            {
                internal const string Login = "/xrpc/com.atproto.server.createSession";
                internal const string RefreshSession = "/xrpc/com.atproto.server.refreshSession";
            }

            internal static class AtProtoIdentity
            {
                internal const string ResolveHandle = "/xrpc/com.atproto.identity.resolveHandle";
            }

            internal static class AtProtoRepo
            {
                internal const string CreateRecord = "/xrpc/com.atproto.repo.createRecord";
                internal const string DeleteRecord = "/xrpc/com.atproto.repo.deleteRecord";
                internal const string UploadBlob = "/xrpc/com.atproto.repo.uploadBlob";
                internal const string listRecords = "/xrpc/com.atproto.repo.listRecords";
            }            

            internal static class Bluesky
            {
                internal const string GetAuthorFeed = "/xrpc/app.bsky.feed.getAuthorFeed";
                internal const string GetActorProfile = "/xrpc/app.bsky.actor.getProfile";
                internal const string GetFollowers = "/xrpc/app.bsky.graph.getFollowers";
                internal const string GetFollowing = "/xrpc/app.bsky.graph.getFollows";
            }

        }
        internal static class Types
        {
            internal static class Bluesky
            {

                internal static class Embed
                {

                    internal const string Images = "app.bsky.embed.images";
                    internal const string External = "app.bsky.embed.exterrnal";
                }

                internal static class Feed
                {

                    internal const string Post = "app.bsky.feed.post";
                }
            }
           
        }

        internal class HeaderNames
        {
            internal const string UserAgent = "user-agent";
        }
    }
}
