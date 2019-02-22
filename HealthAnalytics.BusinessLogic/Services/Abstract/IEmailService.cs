using HealthAnalytics.Data.Entities;
using System;
using System.Threading.Tasks;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailConfirmationMessage<TKey>(User<TKey> user, string confirmationUrl) where TKey : struct, IComparable<TKey>;
    }
}
