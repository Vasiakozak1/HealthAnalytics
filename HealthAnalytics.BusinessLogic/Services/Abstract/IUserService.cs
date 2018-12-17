using HealthAnalytics.BusinessLogic.Data.ViewModels;
using HealthAnalytics.BusinessLogic.Models;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        void Register(RegisterModel model);
        LoginViewModel LogIn(string email, string password);
    }
}
