using System.Text.Json.Serialization;

namespace Bsky.Net.Model
{
    public class Record
    {
        [JsonPropertyName("uri")]
        public string Uri { get; set; }

        [JsonPropertyName("cid")]
        public string Cid { get; set; }

        [JsonPropertyName("value")]
        public RecordValue Value { get; set; }
    }

    public class RecordPost
    {
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Facet>? Facets { get; set; }

        [JsonPropertyName("embed")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Embed? Embed { get; set; }

        [JsonPropertyName("$type")] 
        public string Type { get; set; }
    }
}
