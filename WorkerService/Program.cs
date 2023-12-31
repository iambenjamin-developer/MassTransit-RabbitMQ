using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerService.Consumers;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    //MassTransit configuration - START
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<ProductMessageConsumer>();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.Host(hostContext.Configuration["Rabbitmq:Url"]);
                            cfg.ReceiveEndpoint("my-workerservice-subscriber", e =>
                            {
                                e.UseMessageRetry(r =>
                                {
                                    r.Interval(10, TimeSpan.FromSeconds(2));
                                });
                                e.ConfigureConsumer<ProductMessageConsumer>(context);
                                e.UseConcurrencyLimit(1);
                            });
                        });
                    });

                    services.AddMassTransitHostedService();
                    //MassTransit configuration - END

                });
    }
}
