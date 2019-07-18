using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace AspNetCoreDemo.Configs
{
    public static class ApplicationConfig
    {
        public static IConfiguration Config { get; set; }
        //public ApplicationConfig(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(ConfigPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Config = builder.Build();
        //}
        public static T GetAppSettings<T>(string key) where T : class, new()
        {
            var values = new ServiceCollection()
                .AddOptions()
                .Configure<T>(Config.GetSection(key))
                .BuildServiceProvider()
                .GetService<IOptions<T>>()
                .Value;
            return values;
        }
        
        public static string ApplicationName => Config[nameof(ApplicationName)];
        public static string SubVersion => Config[nameof(SubVersion)];
    }
}
