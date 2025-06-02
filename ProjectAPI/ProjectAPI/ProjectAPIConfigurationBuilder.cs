
using Microsoft.Extensions.Configuration;
using System;
namespace ProjectAPI
{
    public class ProjectAPIConfigurationBuilder
    {
        public void BuildProjectAPIConfigurationBuilder()
        {
            ProjectAPIConfiguration.connectionStringSetting = BuilderConnectionStringSetting();
        }
        private ConnectionStringSetting BuilderConnectionStringSetting()
        {
            return new ConnectionStringSetting
            {
                DefaultConnection = GetRequiredConnectionString()
            };
        }
        private static string GetRequiredConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            var config = builder.Build();

            string conn = config.GetConnectionString("DefaultConnection") ?? "";

            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            return conn;
        }
    }
}
