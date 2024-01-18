IConfiguration Configuration = default;
IHostEnvironment HostingEnvironment = default;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        HostingEnvironment = context.HostingEnvironment;
        var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        Configuration = new ConfigurationBuilder()
            .SetBasePath(appPath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json")
            .AddJsonFile(@"Selenium\Elements\ElementsAlura.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddAppDependencies(Configuration);
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.AddJobAndTrigger<AluraAccessJob>(Configuration);

        })
        .AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        services.AddLogging(logger =>
        {
            logger.ClearProviders();
            logger.SetMinimumLevel(LogLevel.Information);
            logger.AddNLog("NLog.config");
        });
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();