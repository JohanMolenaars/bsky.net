namespace Bsky.Net.Model
{
    public record Error(int StatusCode, ErrorDetail? Detail = default)
    {
    }

    public record Success() { }
}
