using System.ComponentModel.DataAnnotations;

namespace HealthAnalytics.BusinessLogic.Data.Models
{
    public class VerifyEmailModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; }
    }
}
