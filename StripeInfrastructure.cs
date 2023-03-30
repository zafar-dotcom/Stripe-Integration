using Stripe;
using Stripe_Integration.Application;
using Stripe_Integration.Contracts;
using System.Runtime.CompilerServices;

namespace Stripe_Integration
{
    public static class StripeInfrastructure
    {
        public static IServiceCollection AddStripeInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            StripeConfiguration.ApiKey = configuration.GetValue<string>("StripeSetting:SecretKey");

            return services
                .AddScoped<CustomerService>()
                .AddScoped<ChargeService>()
                .AddScoped<TokenService>()
                .AddScoped<IStripeAppService, StripeAppServices>();
        
        }   
    }
}
