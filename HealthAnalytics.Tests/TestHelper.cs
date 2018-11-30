using Microsoft.Extensions.Configuration;
using System.IO;
namespace HealthAnalytics.Tests
{
    public static class TestHelper
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("7AE18357-3E4D-483A-B32B-9251BD1EAC28")
                .AddEnvironmentVariables()
                .Build();
        }
    }
}
