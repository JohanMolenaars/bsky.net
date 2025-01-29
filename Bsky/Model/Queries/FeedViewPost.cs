namespace Bsky.Net.Model.Queries
{
    public record FeedViewPost(PostView Post, ReplyRef? Ref, ReasonRepost? Reason);
}
