using HealthAnalytics.Data.UnitOfWork;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public abstract class BaseService
    {
        protected ISMSService smsService;
        protected IEmailService emailService;
        protected IUnitOfWork<ObjectId> unitOfWork;
        protected IConfiguration configuration;

        public BaseService(ISMSService smsService, IEmailService emailService, IUnitOfWork<ObjectId> unitOfWork, IConfiguration configuration)
        {
            this.smsService = smsService;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }
    }
}
