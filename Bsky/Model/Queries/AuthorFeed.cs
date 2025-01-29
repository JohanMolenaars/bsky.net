namespace Bsky.Net.Model.Queries
{
    public record AuthorFeed(FeedViewPost[] Feed, string? Cursor);
}
