using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using HealthAnalytics.Web;
using HealthAnalytics.Web.Controllers;
using Moq;
using HealthAnalytics.Data.UnitOfWork;
using HealthAnalytics.Data.Repositories;
using HealthAnalytics.Data.Entities;
using Microsoft.Extensions.Configuration;

namespace HealthAnalytics.Tests
{
    public class HealthAnalyticsControllerTest
    {
        private IConfiguration configuration;

        public HealthAnalyticsControllerTest()
        {
            configuration = TestHelper.GetConfiguration();
        }



    }
}
