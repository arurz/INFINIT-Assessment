using INFINIT.ServiceLayer.GitHubStatisticsProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using INFINIT.ServiceLayer.LetterFrequencyCountProvider;
using INFINIT.Configurations;

namespace INFINIT.ServiceLayer.DI
{
    public static class ConfigureExtentions
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration config)
            => services.AddScoped<IGitHubStatisticsService, GitHubStatisticsService>()
                       .AddScoped<ILetterFrequencyCounter, LetterFrequencyCounter>();

        public static IConfigurationBuilder AddConfigurations(this IConfigurationBuilder configuration)
            => configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<GitHubApiSettings>()
                .AddEnvironmentVariables();
    }
}
