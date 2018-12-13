using HealthAnalytics.Data.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace HealthAnalytics.Data.Repositories
{
    public class MongoRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey> where TKey : struct, IComparable<TKey>
    {
        IMongoDatabase database;
        IMongoCollection<TEntity> entitiesCollection;
        public MongoRepository(IMongoDatabase database)
        {
            this.database = database;
            string entityName = typeof(TEntity).Name;
            entitiesCollection = database.GetCollection<TEntity>(entityName);
        }

        public void Create(TEntity entity)
        {
            entitiesCollection.InsertOne(entity);
        }

        public TEntity Get(TKey guid)
        {            
            return entitiesCollection.Find(element => element.Guid.CompareTo(guid) == 0)
                                     .FirstOrDefault();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return entitiesCollection.Find(predicate)
                                     .FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return entitiesCollection.AsQueryable();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return entitiesCollection.AsQueryable()
                .Where(predicate);
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            entitiesCollection.DeleteOne(predicate);
        }

        public void Update(TEntity entity)
        {
            entitiesCollection.ReplaceOne(element => element.Guid.CompareTo(entity.Guid) == 0, entity);
        }
    }
}
