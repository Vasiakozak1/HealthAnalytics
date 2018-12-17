using System.Net;
namespace HealthAnalytics.BusinessLogic.Exceptions
{
    public class ElementNotFoundException: ApiException
    {
        public ElementNotFoundException(string elementName): base(string.Format("Could not find {0}", elementName), HttpStatusCode.NotFound)
        {
        }
    }
}
