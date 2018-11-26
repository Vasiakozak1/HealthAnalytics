using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.Repositories;

namespace HealthAnalytics.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
    }
}
