using SixLabors.ImageSharp.Formats.Jpeg;
using System.IO;
using Image = SixLabors.ImageSharp.Image;

namespace Bsky.Net.Helper.Img
{
    internal static class Meteadata
    {
        /// <summary>
        /// Removes metadata (Exif, IPTC, XMP) from the given image bytes.
        /// </summary>
        /// <param name="imageBytes">The byte array representing the image from which metadata should be removed.</param>
        /// <returns>A byte array representing the image without metadata.</returns>
        public static byte[] Remove(byte[] imageBytes)
        {
            using (var image = Image.Load(imageBytes))
            {
                image.Metadata.ExifProfile = null;
                image.Metadata.IptcProfile = null;
                image.Metadata.XmpProfile = null;

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, new JpegEncoder());
                    return ms.ToArray();
                }
            }
        }
    }
}
