using MassTransit;
using WorkerService.Consumers;

try
{

    var host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ProductMessageConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(hostContext.Configuration["Rabbitmq:Url"]);
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddMassTransitHostedService(true);
        })
        .Build();

    await host.RunAsync();
    return 0;
}
catch (Exception ex)
{
    return 1;
}
finally
{
}