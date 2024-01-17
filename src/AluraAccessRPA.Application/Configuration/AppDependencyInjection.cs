using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraAccessRPA.Application.Configuration;

public static class AppDependencyInjection
{
    public static IServiceCollection AddAppDependencies(this IServiceCollection services)
    {
        //! Alterar para repositorio verdadeiro
        //services.AddTransient<IApiExcelExpert, ApiExcelexpert>();
        //services.AddSingleton<IFileService, FileService>();

        return services;
    }
}
