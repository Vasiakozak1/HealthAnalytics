using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using HealthAnalytics.BusinessLogic.Models;
using MongoDB.Bson;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.BusinessLogic;
using HealthAnalytics.BusinessLogic.Data.ViewModels;
using System.Collections.Generic;

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            LoginViewModel loginResult = userService.LogIn(model.Email, model.Password);
            return Ok(loginResult);
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            userService.Register(model);

            return Ok(Constants.GetSuccessfullRegistrationMessage());
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}