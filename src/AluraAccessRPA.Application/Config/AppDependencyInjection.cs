using AluraAccessRPA.Domain.Interfaces;
using AluraAccessRPA.Infrastructure.Context;
using AluraAccessRPA.Infrastructure.Data;
using AluraAccessRPA.Infrastructure.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Data.Common;


namespace AluraAccessRPA.Application.Config;

public static class AppDependencyInjection
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        //! Alterar para repositorio verdadeiro

        var connectionString = Configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AluraDbContext>(x => x.UseSqlite(connectionString));
        services.AddTransient<RepositoryAlura>();
        services.AddSingleton<IDriverFactoryService,DriverFactoryService>();
        //services.AddSingleton<IFileService, FileService>();

        return services;
    }
}
