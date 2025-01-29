using Bsky.Net.Model;
using System.Text.Json.Serialization;

namespace Bsky.Net.Model
{
    public class HandleResolution
    {
        [JsonConstructor]
        public HandleResolution(Did did)
        {
            Did = did;
        }

        public Did Did { get; }
    }
}
