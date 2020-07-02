using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace WebAPI_ADO_Net.Repositories.Base
{
    public class BaseRepository
    {
        private static IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        protected static SqlConnection Connection = new SqlConnection(Configuration.GetConnectionString("LocalConnection"));
    }
}
