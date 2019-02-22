using System;

namespace HealthAnalytics.BusinessLogic.Data.Models
{
    public class ConfirmEmailModel
    {
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverEmail { get; set; }
        public string ConfirmationUrl { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
