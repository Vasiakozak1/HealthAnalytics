using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using HealthAnalytics.BusinessLogic.Models;

namespace HealthAnalytics.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : HealthAnalyticsController
    {
        public AccountController(IUnitOfWork unitOfWork): base(unitOfWork)
        {
        }

        public IActionResult Login(string email, string password)
        {

            return Ok(CreateJWTToken(null));
        }

        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


        }

        private string CreateJWTToken(IEnumerable<Claim> claims)
        {
            var currentDate = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: currentDate,
                claims: claims,
                expires: currentDate.Add(TimeSpan.FromHours(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}