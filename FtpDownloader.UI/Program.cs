using FtpDownloader.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Startup
{
    public class Program
    {
        [STAThread]
        public static void Main1()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<App>();
                })
                .Build();

            var app = host.Services.GetService<App>();
            app?.Run();
        }
    }
}