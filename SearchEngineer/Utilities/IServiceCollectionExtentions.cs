using Microsoft.Extensions.DependencyInjection;
using SearchEngineer.Repositorys;
using SearchEngineer.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineer.Utilities
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddBllClass(this IServiceCollection services)
        {
            services.AddScoped<ISearchKeywordBll, SearchKeywordBll>();

            return services;
        }

        public static IServiceCollection AddRepositoryClass(this IServiceCollection services)
        {
            services.AddScoped<ISearchKeywordRepository, SearchKeywordRepository>();

            return services;
        }
    }
}
