var host = new HostBuilder()
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddHostedService<AkkaService>();
    })
    .ConfigureLogging((hostContext, configLogging) =>
    {
        configLogging.AddConsole();
    })
    .UseConsoleLifetime()
    .Build();

await host.RunAsync();