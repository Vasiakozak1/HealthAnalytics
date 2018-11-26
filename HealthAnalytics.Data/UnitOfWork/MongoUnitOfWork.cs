using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace HealthAnalytics.Data.UnitOfWork
{
    public class MongoUnitOfWork: IUnitOfWork
    {
        IMongoDatabase database;

        IRepository<User> userRepository;

        public MongoUnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("LocalMongoDB");
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return userRepository != null ? userRepository : getRepository<User>();
            }
        }

        IRepository<T> getRepository<T>() where T: Entity
        {
            return new MongoRepository<T>(database);
        }
    }
}
