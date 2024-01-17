

using AluraAccessRPA.Application.Config;

IConfiguration Configuration = default;
IHostEnvironment HostingEnvironment = default;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        HostingEnvironment = context.HostingEnvironment;
        Configuration = new ConfigurationBuilder()
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json")
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