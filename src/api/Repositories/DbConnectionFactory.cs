using System.Data;
using Npgsql;

namespace pet.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        public IDbConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(CreateDbConnectionString());
            connection.Open();
            return connection;
        }

        private static string CreateDbConnectionString()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            var main = databaseUrl.Split("/")[1];
            var user = main.Split(":")[0];
            var pass = main.Split(":")[1].Split("@")[0];
            var host = main.Split("@")[1].Split(":")[0];
            var db = databaseUrl.Split("/")[2];

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = host,
                Port = 5432,
                Username = user,
                Password = pass,
                Database = db
            };

            return builder.ToString();
        }
    }
}
