using HealthAnalytics.BusinessLogic.Data.Models;
using HealthAnalytics.BusinessLogic.Data.ViewModels;
using System.Threading.Tasks;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        Task Register(RegisterModel model);
        LoginViewModel LogIn(string email, string password);
        void VerifyToken(string token, string email);
    }
}
