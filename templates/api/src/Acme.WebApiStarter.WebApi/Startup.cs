using Acme.WebApiStarter.BootStrap;
using Acme.WebApiStarter.WebApi.Filters;
using Acme.WebApiStarter.WebApi.Installers;
using Cortside.Common.BootStrap;
using Cortside.Common.Correlation;
using Cortside.Common.Json;
using Cortside.Common.Messages.Filters;
using Cortside.Health.Controllers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Acme.WebApiStarter.WebApi {
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup {
        private readonly BootStrapper bootstrapper = null;

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) {
            bootstrapper = new DefaultApplicationBootStrapper();
            bootstrapper.AddInstaller(new IdentityServerInstaller());
            bootstrapper.AddInstaller(new NewtonsoftInstaller());
            bootstrapper.AddInstaller(new SubjectPrincipalInstaller());
            bootstrapper.AddInstaller(new SwaggerInstaller());
            Configuration = configuration;
        }

        /// <summary>
        /// Config
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton<ITelemetryInitializer, AppInsightsInitializer>();
            services.AddApplicationInsightsTelemetry(o => {
                o.InstrumentationKey = Configuration["ApplicationInsights:InstrumentationKey"];
                o.EnableAdaptiveSampling = false;
                o.EnableActiveTelemetryConfigurationSetup = true;
            });

            services.AddResponseCaching();
            services.AddResponseCompression(options => {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddCors();

            services.AddControllers(options => {
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.CacheProfiles.Add("default", new CacheProfile {
                    Duration = 30,
                    Location = ResponseCacheLocation.Any
                });
                //https://stackoverflow.com/questions/55127637/globally-modelstate-validation-in-asp-net-core-mvc
                options.Filters.Add<MessageExceptionResponseFilter>();
                options.Conventions.Add(new ApiControllerVersionConvention());
            })
            .AddNewtonsoftJson(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy()));
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter {
                    DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"
                });
                options.SerializerSettings.Converters.Add(new IsoTimeSpanConverter());
            })
            .PartManager.ApplicationParts.Add(new AssemblyPart(typeof(HealthController).Assembly));

            services.AddRouting(options => {
                options.LowercaseUrls = true;
            });

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped(sp => {
                return sp.GetRequiredService<IUrlHelperFactory>().GetUrlHelper(sp.GetRequiredService<IActionContextAccessor>().ActionContext);
            });

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddPolicyServerRuntimeClient(Configuration.GetSection("PolicyServer"))
                .AddAuthorizationPermissionPolicies();

            services.AddSingleton(Configuration);
            bootstrapper.InitIoCContainer(Configuration as IConfigurationRoot, services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseMiniProfiler();
            app.UseMiddleware<CorrelationMiddleware>();

            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Acme.WebApiStarter Api v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "Acme.WebApiStarter Api v2");
                options.RoutePrefix = "swagger";
            });

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseCors(builder => builder
                .WithOrigins(Configuration.GetSection("Cors").GetSection("Origins").Get<string[]>())
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
