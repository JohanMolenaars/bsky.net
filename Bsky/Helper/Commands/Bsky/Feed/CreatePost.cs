namespace Bsky.Net.Helper.Commands.Bsky.Feed
{
    /// <summary>
    /// A command to create a post
    /// </summary>
    /// <param name="Text">The text</param>
    public record CreatePost(  string Text, (string, string?)[]? images)
    {
    }
}
