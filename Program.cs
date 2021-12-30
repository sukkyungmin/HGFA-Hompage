using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace HangilFA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.CaptureStartupErrors(true)
                    .UseSetting("detailedErrors", "true")
                    .UseStartup<Startup>();

                    webBuilder.UseIISIntegration();


                    //webBuilder.UseStartup<Startup>();
                });
    }
}
