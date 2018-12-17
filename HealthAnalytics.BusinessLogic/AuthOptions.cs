using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HealthAnalytics.BusinessLogic
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer";
        public const string AUDIENCE = "http://localhost"; 
        const string KEY = "mysupersecret_secretkey!123";   
        public const int LIFETIME = 1; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
