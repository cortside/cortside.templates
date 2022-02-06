using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Acme.WebApiStarter.WebApi.Filters;
using Cortside.Common.BootStrap;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Acme.WebApiStarter.WebApi.Installers {
    public class SwaggerInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfigurationRoot configuration) {
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = ApiVersion.Default;
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Version = "v1",
                    Title = "Acme.WebApiStarter API",
                    Description = "Acme.WebApiStarter API",
                });

                c.SwaggerDoc("v2", new OpenApiInfo {
                    Version = "v2",
                    Title = "Acme.WebApiStarter API",
                    Description = "Acme.WebApiStarter API",
                });

                c.OperationFilter<RemoveVersionFromParameter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();

                c.DocInclusionPredicate((version, desc) => {
                    if (!desc.TryGetMethodInfo(out var methodInfo))
                        return false;

                    var versions = methodInfo
                       .DeclaringType?
                   .GetCustomAttributes(true)
                   .OfType<ApiVersionAttribute>()
                   .SelectMany(attr => attr.Versions);

                    var maps = methodInfo
                       .GetCustomAttributes(true)
                   .OfType<MapToApiVersionAttribute>()
                   .SelectMany(attr => attr.Versions)
                   .ToList();

                    return versions?.Any(v => $"v{v}" == version) == true
                             && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });

                c.IgnoreObsoleteActions();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}
