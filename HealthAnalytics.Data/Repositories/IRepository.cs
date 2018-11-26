using System;
using System.Collections.Generic;
using HealthAnalytics.Data.Entities;

namespace HealthAnalytics.Data.Repositories
{
    public interface IRepository<T> where T: Entity
    {
        T Get(Guid guid);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetAll(Func<T, bool> predicate);

        void Create(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
