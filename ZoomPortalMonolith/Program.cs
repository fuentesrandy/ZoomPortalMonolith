using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace ZoomPortalMonolith.WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
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
                .UseStartup<Startup>();
    }
}
