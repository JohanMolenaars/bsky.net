using System.Text.Json.Serialization;

namespace Bsky.Net.Model
{
    public class ListRecordsResponse
    {
        [JsonPropertyName("records")]
        public List<Record> Records { get; set; }
        [JsonPropertyName("cursor")]
        public string? Cursor { get; set; }
    }
}
