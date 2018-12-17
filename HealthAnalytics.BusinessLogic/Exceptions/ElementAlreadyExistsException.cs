using System.Net;

namespace HealthAnalytics.BusinessLogic.Exceptions
{
    public class ElementAlreadyExistsException: ApiException
    {
        public ElementAlreadyExistsException(string elementName): base(string.Format("Such {0} already exists", elementName), HttpStatusCode.BadRequest)
        {
        }
    }
}
