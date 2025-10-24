using System.Configuration;
using System.Data.SqlClient;

namespace PDVnet.GestaoProdutos.Data
{
    public static class ConnectionHelper
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["PDVnetDB"].ConnectionString;
            return new SqlConnection(connectionString);
        }
    }
}
