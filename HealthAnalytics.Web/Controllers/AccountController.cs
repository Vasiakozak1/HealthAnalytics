using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using HealthAnalytics.BusinessLogic.Models;
using MongoDB.Bson;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.BusinessLogic;
using HealthAnalytics.BusinessLogic.Data.ViewModels;

namespace HealthAnalytics.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : HealthAnalyticsController
    {
        private IUserService userService;
        public AccountController(IUnitOfWork<ObjectId> unitOfWork, IUserService userService): base(unitOfWork)
        {
            this.userService = userService;
        }

        public IActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginViewModel loginResult = userService.LogIn(model.Email, model.Password);
            return Ok(loginResult);
        }

        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userService.Register(model);

            return Ok(Constants.USER_REGISTRATED_MESSAGE);
        }        
    }
}