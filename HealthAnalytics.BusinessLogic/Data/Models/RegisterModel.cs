using System.ComponentModel.DataAnnotations;

namespace HealthAnalytics.BusinessLogic.Data.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(maximumLength: 32, MinimumLength = 2)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(maximumLength: 32, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        public string BirthDate { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(pattern: "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])([a-zA-Z0-9]{8,})$", ErrorMessage = "Password should contain at least 8 characters, at least 1 digit and at least one special character")]
        public string Password { get; set; }
    }
}
