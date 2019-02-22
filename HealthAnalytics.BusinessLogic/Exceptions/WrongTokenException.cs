namespace HealthAnalytics.BusinessLogic.Exceptions
{
    public class WrongTokenException: ApiException
    {
        public WrongTokenException(): base("Wrong token", System.Net.HttpStatusCode.BadRequest)
        {
        }
    }
}
