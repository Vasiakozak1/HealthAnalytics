using HealthAnalytics.Data.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthAnalytics.Data.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : Entity
    {
        private IMongoDatabase database;
        public MongoRepository(IMongoDatabase database)
        {
            this.database = database;
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(Guid guid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
