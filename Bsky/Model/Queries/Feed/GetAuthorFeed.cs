namespace Bsky.Net.Model.Queries.Feed
{
    public record GetAuthorFeed(AtUri Actor, int Limit, string? Cursor = default);
}
