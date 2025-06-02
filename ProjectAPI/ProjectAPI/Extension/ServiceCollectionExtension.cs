using ProjectAPI.Model;
using System.Runtime.CompilerServices;

namespace ProjectAPI.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddProjectAPISetting(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SourceSettings>(configuration.GetSection(nameof(SourceSettings)));
            service.Configure<TimeSettings>(configuration.GetSection(nameof(TimeSettings)));
            service.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
            return service;
        }
    }
}
