namespace HealthAnalytics.BusinessLogic.Exceptions
{
    public class WrongCredentialsException: ApiException
    {
        public WrongCredentialsException() : base("Could not find a user with such email and password", System.Net.HttpStatusCode.NotFound)
        {
        }
    }
}
