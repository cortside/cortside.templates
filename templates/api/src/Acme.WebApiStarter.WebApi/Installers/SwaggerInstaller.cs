﻿using System;
using System.IO;
using System.Reflection;
using Acme.WebApiStarter.WebApi.Filters;
using Cortside.Common.BootStrap;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Acme.WebApiStarter.WebApi.Installers {
    public class SwaggerInstaller : IInstaller {
        public void Install(IServiceCollection services, IConfigurationRoot configuration) {
            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = false;
                o.UseApiBehavior = true;
            });

            services.AddVersionedApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = false;
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

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.OperationFilter<RemoveVersionFromParameter>();
                c.OperationFilter<AuthorizeOperationFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                c.IgnoreObsoleteActions();
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }
    }
}