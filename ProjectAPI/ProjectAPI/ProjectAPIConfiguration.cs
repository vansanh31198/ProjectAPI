using System.ComponentModel.DataAnnotations;

namespace ProjectAPI
{
    public class ProjectAPIConfiguration
    {
        public static ConnectionStringSetting? connectionStringSetting { get; set; }
    }

    public class ConnectionStringSetting
    {
        public string DefaultConnection { get; set; } = "";
    }
}
