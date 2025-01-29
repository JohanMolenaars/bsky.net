using System.Text;

namespace Bsky.Net.Helper.Strings
{
    /// <summary>
    /// Represents a Unicode string with both UTF-16 and UTF-8 encodings.
    /// </summary>
    public class UnicodeString
    {
        /// <summary>
        /// Gets or sets the UTF-16 encoded string.
        /// </summary>
        public string Utf16 { get; set; }

        /// <summary>
        /// Gets or sets the UTF-8 encoded byte array.
        /// </summary>
        public byte[] Utf8 { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnicodeString"/> class with the specified UTF-16 string.
        /// </summary>
        /// <param name="utf16">The UTF-16 encoded string.</param>
        public UnicodeString(string utf16)
        {
            Utf16 = utf16;
            Utf8 = Encoding.UTF8.GetBytes(utf16);
        }

        /// <summary>
        /// Converts a UTF-16 code-unit offset to a UTF-8 code-unit offset.
        /// </summary>
        /// <param name="i">The UTF-16 code-unit offset.</param>
        /// <returns>The corresponding UTF-8 code-unit offset.</returns>
        public int Utf16IndexToUtf8Index(int i)
        {
            return Encoding.UTF8.GetByteCount(Utf16.Substring(0, i));
        }
    }
}
