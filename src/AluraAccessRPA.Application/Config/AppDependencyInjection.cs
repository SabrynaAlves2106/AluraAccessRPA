namespace AluraAccessRPA.Application.Config;

public static class AppDependencyInjection
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddSingleton(Configuration);
        //configuração para o entity
        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AluraDbContext>(x => x.UseSqlite(connectionString));
        
        services.AddTransient<IRepository,RepositoryAlura>();
        services.AddSingleton<IDriverFactoryService,DriverFactoryService>();
        services.AddSingleton<INavigator, Navigator>();

        services.AddSingleton<ControllerAlura>();
        services.AddSingleton<AluraPage>();

        return services;
    }
}
