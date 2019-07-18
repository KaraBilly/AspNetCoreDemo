using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Extensions.DependencyInjection;
using AspNetCoreDemo.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreDemo
{
    public class Startup
    {
        #region Fileds
        private const string SwaggerJsonPath = "/swagger/v1/swagger.json";
        private const string MetadataTitle = "AspNetCoreDemo";
        private const string Version = "v1";
        #endregion

        #region Properties
        private static string ConfigPath => Path.Combine(
            PlatformServices.Default.Application.ApplicationBasePath,
            "Configs", "Contents");
        /// <summary>
        ///     The path for service metadata.
        /// </summary>
        private string ServiceXmlPath => Path.Combine(
            PlatformServices.Default.Application.ApplicationBasePath,
            $"{ApplicationConfig.ApplicationName}.xml");
        private string ServiceDtoXmlPath => Path.Combine(
            PlatformServices.Default.Application.ApplicationBasePath,
            $"{ApplicationConfig.ApplicationName}.Dtos.xml");
        //private ApplicationConfig ApplicationConfig { get; }
        private IHostingEnvironment CurrentEnvironment { get; }
        #endregion
        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
            //ApplicationConfig = new ApplicationConfig(env);
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Version, new Info
                {
                    Title = MetadataTitle,
                    //Description = _introduction,
                    //Contact = new Contact
                    //{
                    //    Email = ContactMail,
                    //    Name = ContactNames
                    //},
                    Version = Version + ApplicationConfig.SubVersion
                });

                //Enable Xml Document
                options.IncludeXmlComments(ServiceXmlPath);
                options.IncludeXmlComments(ServiceDtoXmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            RegisterConfiguration(services);
            AddServicesFeatures(services);
            ConfigureSwagger(services);
            return BuildAspectCore(services);
        }
        private static IServiceProvider BuildAspectCore(IServiceCollection services)
        {
            services.ConfigureDynamicProxy();
            return services.BuildAspectInjectorProvider();
        }
        private void RegisterConfiguration(IServiceCollection services)
        {
            var root = new ConfigurationBuilder()
                .SetBasePath(ConfigPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{CurrentEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            ApplicationConfig.Config = root;
            services.AddSingleton(ApplicationConfig.Config);
        }
        private static void AddServicesFeatures(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    if (opt.SerializerSettings.ContractResolver
                        is DefaultContractResolver resolver)
                        resolver.NamingStrategy = new SnakeCaseNamingStrategy();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMemoryCache();
            services.AddOptions();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "text/plain";
                        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (errorFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500, errorFeature.Error, errorFeature.Error.Message);
                        }

                        await context.Response.WriteAsync("There was an error");
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerJsonPath, "DemoAPI V1");
            });
        }
    }
}
