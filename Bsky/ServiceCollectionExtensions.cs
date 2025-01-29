using Bsky.Net.Helper;
using Bsky.Net.Respository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bsky.Net
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// Adds the Bluesky API services to the specified <see cref="IServiceCollection"/> and configures an <see cref="IHttpClientBuilder"/> for the Bluesky API.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="configurator">An optional action to configure the <see cref="BlueskyApiOptions"/>.</param>
        /// <returns>An <see cref="IHttpClientBuilder"/> that can be used to configure the HTTP client for the Bluesky API.</returns>
        public static IHttpClientBuilder AddBluesky(
            this IServiceCollection services,
            Action<BlueskyApiOptions>? configurator = null)
        {
            var options = new BlueskyApiOptions();
            configurator?.Invoke(options);
            services.TryAddSingleton(options);

            services.AddScoped<IBlueskyApi, BlueskyApi>();
            return services.AddHttpClient(Constants.BlueskyApiClient, (_, client) =>
            {
                client.DefaultRequestHeaders.Add("Accept", Constants.AcceptedMediaType);
                client.BaseAddress = new Uri(options.Url.TrimEnd('/'));
                client.DefaultRequestHeaders.Add(Constants.HeaderNames.UserAgent, options.UserAgent);
            });
        }
    }
}
