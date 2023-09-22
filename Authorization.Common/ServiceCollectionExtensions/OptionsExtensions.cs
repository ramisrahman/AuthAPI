using Authorization.Common.Services.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Authorization.Common.ServiceCollectionExtensions
{
    public static class OptionsExtensions
    {
        public static IServiceCollection AddAuthenticationOptions(this IServiceCollection services)
        {
            services.AddOptions<AuthenticationOptions>()
                .Configure<IConfiguration>((option, configuration) =>
                {
                    option.SecretKey = configuration.GetValue<string>("Authorize:SecretKey");
                    option.Issuer = configuration.GetValue<string>("Authorize:Issuer");
                    option.Audience = configuration.GetValue<string>("Authorize:Audience");
                    option.ExpirationMinutes = configuration.GetValue<double>("Authorize:ExpirationMinutes", 30);
                });

            return services;
        }
    }
}
