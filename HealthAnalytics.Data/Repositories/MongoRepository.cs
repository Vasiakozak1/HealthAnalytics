using HealthAnalytics.Data.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace HealthAnalytics.Data.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : Entity
    {
        IMongoDatabase database;
        IMongoCollection<T> entitiesCollection;
        public MongoRepository(IMongoDatabase database)
        {
            this.database = database;
            string entityName = typeof(T).Name;
            entitiesCollection = database.GetCollection<T>(entityName);
        }

        public void Create(T entity)
        {
            entitiesCollection.InsertOne(entity);
        }

        public T Get(Guid guid)
        {
            return entitiesCollection.Find(element => element.Guid == guid)
                                     .FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return entitiesCollection.AsQueryable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return entitiesCollection.AsQueryable()
                .Where(predicate);
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            entitiesCollection.DeleteOne(predicate);
        }

        public void Update(T entity)
        {
            entitiesCollection.ReplaceOne(element => element.Guid == entity.Guid, entity);
        }
    }
}
