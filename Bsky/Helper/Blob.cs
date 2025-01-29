using System.Text.RegularExpressions;

namespace Bsky.Net.Helper
{
    internal class BlobConverter
    {

        /// <summary>
        /// Converts a Data URI to a byte array.
        /// </summary>
        /// <param name="dataUri">The Data URI to convert. It should be a valid Base64 encoded string.</param>
        /// <returns>A byte array representing the decoded data from the Data URI.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the <paramref name="dataUri"/> is null, empty, or not in a valid Data URI format.
        /// </exception>
        public byte[] ConvertDataURIToByteArray(string dataUri)
        {
            if (string.IsNullOrWhiteSpace(dataUri))
            {
            throw new ArgumentException("Data URI cannot be null or empty", nameof(dataUri));
            }

            var base64Data = dataUri;
            if (IsBase64Image(base64Data))
            {
            base64Data = dataUri.Split(',')[1];
            }
            else
            {
            throw new ArgumentException("Invalid Data URI format", nameof(dataUri));
            }

            try
            {
            return Convert.FromBase64String(base64Data);
            }
            catch (FormatException ex)
            {
            throw new ArgumentException("Data URI is not a valid Base64 string", nameof(dataUri), ex);
            }
        }

        internal bool IsBase64Image(string base64String)
        {
            if (string.IsNullOrWhiteSpace(base64String))
            {
            return false;
            }

            string pattern = @"^data:image\/(jpeg|png|gif|bmp|webp);base64,[A-Za-z0-9+/]+={0,2}$";
            return Regex.IsMatch(base64String, pattern);
        }
    }
}
