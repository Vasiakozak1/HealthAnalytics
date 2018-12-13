using HealthAnalytics.Data.UnitOfWork;
using MongoDB.Bson;

namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public abstract class BaseService
    {
        protected ISMSService smsService;
        protected IEmailService emailService;
        protected IUnitOfWork<ObjectId> unitOfWork;

        public BaseService(ISMSService smsService, IEmailService emailService, IUnitOfWork<ObjectId> unitOfWork)
        {
            this.smsService = smsService;
            this.emailService = emailService;
            this.unitOfWork = unitOfWork;
        }
    }
}
