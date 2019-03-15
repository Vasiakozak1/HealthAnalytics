using HealthAnalytics.BusinessLogic.Data.Models;
using HealthAnalytics.BusinessLogic.Data.ViewModels;
using HealthAnalytics.BusinessLogic.Exceptions;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HealthAnalytics.BusinessLogic.Services.Implementation
{
    public class UserService : BaseService, IUserService
    {
        private IHashingService hashingService;
        public UserService(ISMSService smsService, 
            IEmailService emailService, 
            IUnitOfWork<ObjectId> unitOfWork, 
            IHashingService hashingService,
            IConfiguration configuration) 
            : base(smsService, emailService, unitOfWork, configuration)
        {
            this.hashingService = hashingService;
        }

        public LoginViewModel LogIn(string email, string password)
        {
            User<ObjectId> user = unitOfWork.UserRepository.Get(u => u.Email == email);
            if (user != null && user.IsActivated)
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
        
        public void VerifyToken(string token, string email)
        {
            var userToActivate = unitOfWork.UserRepository.Get(u => u.Email == email);
            if(userToActivate != null)
            {
                var userToken = unitOfWork.TokenRepository.Get(t => t.UserId == userToActivate.Id);
                if (token.Equals(userToken.Token))
                {
                    userToActivate.IsActivated = true;
                    unitOfWork.TokenRepository.Remove(t => t.UserId == userToActivate.Id);
                    unitOfWork.UserRepository.Update(userToActivate);
                    return;
                }
            }
            throw new WrongTokenException();
        }

        public Task Register(RegisterModel model)
        {
            var userExists = unitOfWork.UserRepository.Get(u => u.Email.Equals(model.Email)) != null;
            if (!userExists)
            {
                var birthDate = DateTime.Parse(model.BirthDate);
                var user = new User<ObjectId>
                {
                    Id = ObjectId.GenerateNewId(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Gender = model.Gender,
                    BirthDate = birthDate,
                    IsActivated = false,
                    PasswordHash = hashingService.GetHash(model.Password)
                };

                var userToken = generateUserToken(user);
                var confirmationUrl = GenerateEmailConfirmationUrl(userToken.Token, user.Email);
                emailService.SendEmailConfirmationMessage(user, confirmationUrl).Wait();
                unitOfWork.UserRepository.Create(user);
                unitOfWork.TokenRepository.Create(userToken);
            }
            else
            {
                throw new ElementAlreadyExistsException(string.Format("User with email: {0}", model.Email));
            }
            return Task.CompletedTask;
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

        private string GenerateEmailConfirmationUrl(string token, string email)
        {
            IConfigurationSection clientSection = configuration.GetSection(Constants.CLIENT_SECTION);
            string clientBaseUrl = clientSection[Constants.CLIENT_URL];
            string confirmEmailUrl = clientSection[Constants.CONFIRM_EMAIL_URL];
            return string.Format("{0}/{1}?email={2}&token={3}", clientBaseUrl, confirmEmailUrl, email, token);
        }

        private UserToken<ObjectId> generateUserToken(User<ObjectId> user)
        {
            string token = hashingService.GetHash(user.PasswordHash + DateTime.Now.ToLongDateString());
            return new UserToken<ObjectId>
            {
                DateExpired = DateTime.Now.AddHours(Constants.USER_REGISTER_TOKEN_EXPIRE_HOURS),
                Token = token,
                UserId = user.Id
            };
        }
    }
}
