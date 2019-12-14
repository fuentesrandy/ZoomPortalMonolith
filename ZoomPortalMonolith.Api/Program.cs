using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ZoomPortalMonolith.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
#if DEBUG == false
         .ConfigureAppConfiguration((context, config) =>
              {
                  var builtConfig = config.Build();
                  config.AddAzureKeyVault(
                      $"https://{builtConfig["KeyVault:Vault"]}.vault.azure.net/",
                      builtConfig["KeyVault:ClientId"],
                      builtConfig["KeyVault:ClientSecret"],
                      new DefaultKeyVaultSecretManager());
              }) 
#endif
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
