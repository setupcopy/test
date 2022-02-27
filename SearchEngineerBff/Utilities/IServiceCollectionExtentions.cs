using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchEngineerBackendForFrontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using static SearchEngineerProto.ISearchKeyword;

namespace SearchEngineerBackendForFrontend.Utilities
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection InitialGrpcClient(this IServiceCollection services, IConfiguration configuration)
        {
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            services.AddGrpcClient<ISearchKeywordClient>(options =>
            {
                options.Address = new Uri(configuration["SearchEngineerUrl"]);
            });

            return services;
        }

        public static IServiceCollection AddServicesClass(this IServiceCollection services)
        {
            services.AddScoped<ISearchKeywordService, SearchKeywordService>();

            return services;
        }
    }
}
