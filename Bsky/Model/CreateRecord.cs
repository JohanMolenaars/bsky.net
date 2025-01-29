using Bsky.Net.Model;

namespace Bsky.Net.Model
{
    public record CreateRecord(string Collection, string Repo, RecordPost Record)
    {
    }
}
