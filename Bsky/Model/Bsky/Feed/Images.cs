namespace Bsky.Net.Model.Bsky.Feed
{
    public class Img
    {

        /// <summary>
        /// The features of the Facet
        /// </summary>
        public  List<Dictionary<string, string?>> Images { get; }
        protected void AddFeature(string key, string? value)
        {
            Images.First().Add(key, value);
        }
    }
}
