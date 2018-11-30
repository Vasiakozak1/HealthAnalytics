using HealthAnalytics.Data.Entities;
using HealthAnalytics.Data.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthAnalytics.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthAnalyticsController : ControllerBase
    {
        protected IUnitOfWork unitOfWork;
        public HealthAnalyticsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }        
    }
}