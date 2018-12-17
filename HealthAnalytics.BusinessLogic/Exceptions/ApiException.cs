using System;
using System.Net;

namespace HealthAnalytics.BusinessLogic.Exceptions
{
    public abstract class ApiException: Exception
    {
        public HttpStatusCode StatusCode { get; private set; }

        protected ApiException(string message, HttpStatusCode statusCode): base(message)
        {
            StatusCode = statusCode;
        }
    }
}
