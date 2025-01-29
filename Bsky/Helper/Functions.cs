namespace Bsky.Net.Helper
{
    /// <summary>
    /// Provides helper functions for formatting values.
    /// </summary>
    internal static class Functions
    {
        /// <summary>
        /// Formats the specified value as a string that includes the type name and the value itself.
        /// </summary>
        /// <typeparam name="T">The type of the value to format.</typeparam>
        /// <param name="value">The value to format.</param>
        /// <returns>A string that represents the formatted value.</returns>
        internal static string FormatValue<T>(T value) => $"{typeof(T).FullName}: {value?.ToString()}";
    }
}
