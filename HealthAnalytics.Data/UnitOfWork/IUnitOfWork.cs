using System;
using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.Repositories;

namespace HealthAnalytics.Data.UnitOfWork
{
    public interface IUnitOfWork<TEntitiesKey> where TEntitiesKey: struct, IComparable<TEntitiesKey>
    {
        IRepository<User<TEntitiesKey>, TEntitiesKey> UserRepository { get; }
        IRepository<UserToken<TEntitiesKey>, TEntitiesKey> TokenRepository { get; }
    }
}
