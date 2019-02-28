using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.BusinessLogic;
using HealthAnalytics.BusinessLogic.Data.ViewModels;
using System.Collections.Generic;
using HealthAnalytics.BusinessLogic.Data.Models;

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

        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(LoginModel model)
        {
            LoginViewModel loginResult = userService.LogIn(model.Email, model.Password);
            return Ok(loginResult);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(RegisterModel model)
        {
            userService.Register(model);

            return Ok(Constants.GetSuccessfullRegistrationMessage());
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult VerifyEmail(VerifyEmailModel model)
        {
            userService.VerifyToken(model.Token, model.Email);
            return Ok(Constants.GetSuccessfullEmailConfirmationMessage());
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}