using HealthAnalytics.BusinessLogic.Models;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.UnitOfWork;
using MongoDB.Bson;
using System;

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

        public ServiceResult<string> Register(RegisterModel model)
        {
            var birthDate = DateTime.Parse(model.BirthDate);
            var userExists = unitOfWork.UserRepository.Get(u => u.Email.Equals(model.Email)) != null;
            if (userExists)
            {
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
                return new ServiceResult<string>(IsSuccess: true, Data: "");
            }
            else
            {

            }
        }

        public ServiceResult<string> Login(LoginModel model)
        {
            throw new NotImplementedException();
        }
    }
}
