﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.Extensions.DependencyInjection;
using ND.ManagementSvcs.Configs;
using ND.ManagementSvcs.Filters;
using ND.ManagementSvcs.Framework.Infrastructures.Cache;
using ND.ManagementSvcs.Framework.Repositories;
using ND.ManagementSvcs.Framework.Repositories.Entities.GroupShopping;
using ND.ManagementSvcs.Framework.Repositories.Interfaces;
using ND.ManagementSvcs.Mapping;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;
using NLog.Web;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ND.ManagementSvcs
{
    public class Startup
    {
        #region Fileds
        private const string SwaggerJsonPath = "/swagger/v1/swagger.json";
        private const string MetadataTitle = "ND.ManagementSvcs";
        private const string Version = "v1";
        private const string DefaultContentType = "application/json";
        //public const string ContactMail = ""
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
        private static IMemoryCache AppMemoryCache { get; set; }
        private static IMemoryCacheObjectManager AppCacheObjectManager { get; set; }
        private static IValuesRepository CurrentValuesRepository { get; set; }
        private static IGroupShoppingRepository CurrentGroupShoppingRepository { get; set; }
        #endregion
        public Startup(IHostingEnvironment env)
        {
            CurrentEnvironment = env;
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
            RegisterCacheManager(services);
            InitializeAutoMapper(services);
            InitializeOrmConfiguration(services);
            RegisterRepositories(services);
            RegisterModules(services);
            RegisterBusinessProviders(services);
            ConfigureSwagger(services);
            return BuildAspectCore(services);
        }

        private void InitializeAutoMapper(IServiceCollection services)
        {
            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ValuesProfile>();
                cfg.AddProfile<GroupShoppingProfile>();
            });
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();
        }

        private void InitializeOrmConfiguration(IServiceCollection services)
        {
            services.AddDbContext<OrderContext>(opt =>
                opt.UseMySQL(ApplicationConfig.MySqlConnectionStrings.DefaultConnection));
        }
        private void RegisterRepositories(IServiceCollection services)
        {
            CurrentValuesRepository = new ValueRepository(AppCacheObjectManager);
            services.AddSingleton(CurrentValuesRepository);
            services.AddScoped<IGroupShoppingRepository, GroupShoppingRepository>();
            //CurrentGroupShoppingRepository = new GroupShoppingRepository(new OrderContext(ApplicationConfig.MySqlConnectionStrings.DefaultConnection));
            //services.AddSingleton(CurrentGroupShoppingRepository);
        }
        private void RegisterModules(IServiceCollection services)
        {
            //throw new NotImplementedException();
        }

        private void RegisterBusinessProviders(IServiceCollection services)
        {
            //throw new NotImplementedException();
        }

        

        private void RegisterHttpClientFactory(IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddHttpClient("github", c =>
            {
                c.BaseAddress = new Uri("https://api.github.com/");
                c.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                c.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            });
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
            services.AddMvc(opt =>
                {
                    opt.Filters.Add<GlobalExceptionFilterAttribute>(); 
                })
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
        private static void RegisterCacheManager(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            AppMemoryCache = provider.GetService<IMemoryCache>();
            AppCacheObjectManager = new MemoryCacheObjectManager(
                AppMemoryCache ?? throw new NullReferenceException());
            services.AddSingleton(AppCacheObjectManager);
        }

        private static void RegisterDistributedCache(IServiceCollection services)
        {
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = "127.0.0.1:6379";
            });
        }

        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseExceptionHandler(
                options =>
                {
                    options.Run(
                        async context =>
                        {
                            var r = context.Response;
                            r.StatusCode = (int)HttpStatusCode.InternalServerError;
                            r.ContentType = DefaultContentType;
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex?.Error != null)
                            {
                                await context.Response.WriteAsync(JsonConvert.SerializeObject(
                                    new
                                    {
                                        ErrorMessage = ex.Error.Message,
                                        ex.Error.StackTrace
                                    })).ConfigureAwait(false);
                            }
                        });
                }
            );

            var nLogFile = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                "Configs",
                "NLog.config");
            env.ConfigureNLog(nLogFile);
            LogManager.Configuration.Variables["ConnectionStrings"] =
                ApplicationConfig.MySqlConnectionStrings.DefaultConnection;

            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SwaggerJsonPath, MetadataTitle);
            });
        }
    }
}
