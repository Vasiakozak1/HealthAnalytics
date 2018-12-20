using HealthAnalytics.BusinessLogic.Data.ViewModels;
using HealthAnalytics.BusinessLogic.Exceptions;
using HealthAnalytics.BusinessLogic.Models;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HealthAnalytics.BusinessLogic.Services.Implementation
{
    public class UserService : BaseService, IUserService
    {
        private IHashingService hashingService;
        public UserService(ISMSService smsService, IEmailService emailService, IUnitOfWork<ObjectId> unitOfWork, IHashingService hashingService) 
            : base(smsService, emailService, unitOfWork)
        {
            this.hashingService = hashingService;
        }

        public LoginViewModel LogIn(string email, string password)
        {
            User<ObjectId> user = unitOfWork.UserRepository.Get(u => u.Email == email);
            if (user != null)
            {
                bool rightPassword = hashingService.VerifyHash(password, user.PasswordHash);
                if (rightPassword)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim("First name", user.FirstName),
                        new Claim("Last name", user.LastName),
                        new Claim("Email", user.Email)
                    };
                    string token = CreateJWTToken(claims);

                    return new LoginViewModel
                    {
                        Email = email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = token
                    };
                }
            }
            throw new WrongCredentialsException();
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

        public void Register(RegisterModel model)
        {
            var userExists = unitOfWork.UserRepository.Get(u => u.Email.Equals(model.Email)) != null;
            if (!userExists)
            {
                var birthDate = DateTime.Parse(model.BirthDate);
                var user = new User<ObjectId>
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Gender = model.Gender,
                    BirthDate = birthDate,
                    IsActivated = false,
                    PasswordHash = hashingService.GetHash(model.Password)
                };
                unitOfWork.UserRepository.Create(user);
            }
            else
            {
                throw new ElementAlreadyExistsException(string.Format("User with email: {0}", model.Email));
            }
        }

        private UserToken<ObjectId> GenerateUserToken(User<ObjectId> user)
        {
            string token = hashingService.GetHash(user.PasswordHash + DateTime.Now.ToLongDateString());
            return new UserToken<ObjectId>
            {
                DateExpired = DateTime.Now.AddHours(Constants.USER_REGISTER_TOKEN_EXPIRE_HOURS),
                Token = token,
                UserId = user.Guid
            };
        }
    }
}
