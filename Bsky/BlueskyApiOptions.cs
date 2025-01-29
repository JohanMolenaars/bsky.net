namespace Bsky.Net
{
    /// <summary>
    /// Represents the configuration options for the Bluesky API.
    /// </summary>
    public class BlueskyApiOptions
    {
        /// <summary>
        /// Gets or sets the base URL for the Bluesky API.
        /// Default value is "https://bsky.social".
        /// </summary>
        public string Url { get; set; } = "https://bsky.social";

        /// <summary>
        /// Gets or sets the User-Agent header value to be used in API requests.
        /// Default value is "BSky.Net".
        /// </summary>
        public string UserAgent { get; set; } = "BSky.Net";

        /// <summary>
        /// Gets or sets a value indicating whether to track the session.
        /// Default value is true.
        /// </summary>
        public bool TrackSession { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether to automatically renew the session.
        /// Default value is false.
        /// </summary>
        public bool AutoRenewSession { get; set; } = false;
    }
}
