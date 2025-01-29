using Bsky.Net.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bsky.Net.Helper.JSON
{
    public class AtUriJsonConverter : JsonConverter<AtUri>
    {
        /// <summary>
        /// Reads and converts the JSON to an <see cref="AtUri"/> object.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">The type of the object to convert.</param>
        /// <param name="options">Options to control the behavior during reading.</param>
        /// <returns>An <see cref="AtUri"/> object if the JSON is valid; otherwise, <c>null</c>.</returns>
        public override AtUri? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (value is null)
            {
                return default;
            }

            try
            {
                return new AtUri(value);
            }
            catch (Exception)
            {
                return default;
            }
        }
        /// <summary>
        /// Writes the JSON representation of the specified <see cref="AtUri"/> value.
        /// </summary>
        /// <param name="writer">The <see cref="Utf8JsonWriter"/> to write to.</param>
        /// <param name="value">The <see cref="AtUri"/> value to convert to JSON.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
        public override void Write(Utf8JsonWriter writer, AtUri? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }

    }
}
