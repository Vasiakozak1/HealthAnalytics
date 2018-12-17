using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;

namespace HealthAnalytics.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthAnalyticsController : ControllerBase
    {
        protected IUnitOfWork<ObjectId> unitOfWork;
        public HealthAnalyticsController(IUnitOfWork<ObjectId> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }        
    }
}