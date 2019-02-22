using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HealthAnalytics.Data.UnitOfWork
{
    public class MongoUnitOfWork: IUnitOfWork<ObjectId>
    {
        IMongoDatabase database;

        IRepository<User<ObjectId>, ObjectId> userRepository;
        IRepository<UserToken<ObjectId>, ObjectId> tokenRepository;

        public MongoUnitOfWork(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("LocalMongoDB");
            var connection = new MongoUrlBuilder(connectionString);
            var client = new MongoClient(connectionString);
            database = client.GetDatabase(connection.DatabaseName);
        }

        public IRepository<User<ObjectId>, ObjectId> UserRepository
        {
            get
            {
                return userRepository != null ? userRepository : getRepository<User<ObjectId>>();
            }
        }

        public IRepository<UserToken<ObjectId>, ObjectId> TokenRepository
        {
            get
            {
                return tokenRepository != null ? tokenRepository : getRepository<UserToken<ObjectId>>();
            }
        }

        IRepository<T, ObjectId> getRepository<T>() where T: Entity<ObjectId>
        {
            return new MongoRepository<T, ObjectId>(database);
        }
    }
}
