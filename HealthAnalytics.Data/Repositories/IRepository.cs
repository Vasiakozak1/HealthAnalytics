using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using HealthAnalytics.Data.Entities;

namespace HealthAnalytics.Data.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity: Entity<TKey> where TKey: struct, IComparable<TKey>
    {
        TEntity Get(TKey guid);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Remove(Expression<Func<TEntity, bool>> predicate);
    }
}
