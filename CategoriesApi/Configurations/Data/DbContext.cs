using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace CategoriesApi.Configurations.Data
{
    public interface IDbContext
    {
        IDbConnection CreateConnection();
    }

    public class DbContext : IDbContext
    {
        readonly IOptions<ConnectionStrings> _connection;

        public DbContext(IOptions<ConnectionStrings> connection)
        {
            _connection = connection;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connection.Value.MSSQL);
        }

    }
}
