namespace Bsky.Net.Helper
{
    /// <summary>
    /// Provides extension methods for handling <see cref="ArgumentNullException"/>.
    /// </summary>
    internal static class ArgumentNullExceptionExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the specified argument is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="t">The argument to check for null.</param>
        /// <returns>The original argument if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the argument is null.</exception>
        internal static T ThrowIfNull<T>(this T? t)
        {
            ArgumentNullException.ThrowIfNull(t);
            return t;
        }
    }
}
