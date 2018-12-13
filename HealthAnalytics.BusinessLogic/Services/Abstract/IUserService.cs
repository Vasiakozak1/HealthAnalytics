using HealthAnalytics.BusinessLogic.Models;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public interface IUserService
    {
        ServiceResult<string> Register(RegisterModel model);
        ServiceResult<string> Login(LoginModel model);
    }
}
