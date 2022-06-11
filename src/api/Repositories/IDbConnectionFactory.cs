using System.Data;

namespace pet.Repositories
{
    public interface IDbConnectionFactory
    {
        public IDbConnection CreateConnection();
    }
}
