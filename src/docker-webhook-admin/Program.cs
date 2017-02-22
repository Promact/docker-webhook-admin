using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Docker.Webhook.Admin
{
    public class Program
    {
        protected Program()
        {
            //This will eliminate instansiation of Program class.
        }
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }    
}
