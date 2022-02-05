using Acme.WebApiStarter.UserClient;
using Cortside.Common.BootStrap;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.WebApiStarter.BootStrap.Installer {
    public class CatalogClientInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfigurationRoot configuration) {
            services.AddScoped<ICatalogClient, CatalogClient>();
            var catalogClientConfiguration = configuration.GetSection("CatalogApi").Get<CatalogClientConfiguration>();
            services.AddSingleton(catalogClientConfiguration);
        }
    }
}
