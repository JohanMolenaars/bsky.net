using Bsky.Net.Model;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bsky.Net.Helper.JSON
{
    public class DidJsonConverter : JsonConverter<Did>
    {
        /// <summary>
        /// Reads and converts the JSON to a <see cref="Did"/> object.
        /// </summary>
        /// <param name="reader">The <see cref="Utf8JsonReader"/> to read from.</param>
        /// <param name="typeToConvert">The type of the object to convert.</param>
        /// <param name="options">Options to control the behavior during reading.</param>
        /// <returns>
        /// A <see cref="Did"/> object if the JSON contains a valid string representation of a Did; otherwise, null.
        /// </returns>
        public override Did? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();
            if (value is null)
            {
                return default;
            }

            try
            {
                return new Did(value);
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
        /// <param name="value">The <see cref="Did"/> value to convert to JSON.</param>
        /// <param name="options">The <see cref="JsonSerializerOptions"/> to use.</param>
        public override void Write(Utf8JsonWriter writer, Did? value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
