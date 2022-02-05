using System;
using System.IdentityModel.Tokens.Jwt;
using Cortside.Common.BootStrap;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acme.WebApiStarter.WebApi.Installers {
    public class IdentityServerInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfigurationRoot configuration) {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var authConfig = configuration.GetSection("IdentityServer");
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options => {
                    // base-address of your identityserver
                    options.Authority = authConfig.GetValue<string>("Authority");
                    options.RequireHttpsMetadata = authConfig.GetValue<bool>("RequireHttpsMetadata");
                    options.RoleClaimType = "role";
                    options.NameClaimType = "name";

                    // name of the API resource
                    options.ApiName = authConfig.GetValue<string>("ApiName");
                    options.ApiSecret = authConfig.GetValue<string>("ApiSecret");

                    options.EnableCaching = true;
                    options.CacheDuration = TimeSpan.FromMinutes(10);
                });
        }
    }
}
