using Acme.WebApiStarter.Configuration;
using Cortside.Common.BootStrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.WebApiStarter.BootStrap.Installer {
    public class EncryptionInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfigurationRoot configuration) {
            var config = configuration.GetSection("Encryption").Get<EncryptionConfiguration>();
            services.AddSingleton(config);
        }
    }
}
