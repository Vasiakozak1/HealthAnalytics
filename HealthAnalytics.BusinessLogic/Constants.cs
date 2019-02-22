using HealthAnalytics.BusinessLogic.Data.ViewModels;

namespace HealthAnalytics.BusinessLogic
{
    public static class Constants
    {
        public const double USER_REGISTER_TOKEN_EXPIRE_HOURS = 3;
        public const string TEMPLATES_FOLDER_NAME = "Templates";
        public const string CONFIRM_EMAIL_TEMPLATE_NAME = "ConfirmEmail.cshtml";

        public const string CONFIRM_EMAIL_MESSAGE_SUBJECT = "Email confirmation for registration in <App name>";

        public const string CLIENT_SECTION = "Client";
        public const string CLIENT_URL = "clientUrl";
        public const string CONFIRM_EMAIL_URL = "confirmEmailUrl";

        public static MessageViewModel GetSuccessfullRegistrationMessage()
        {
            return new MessageViewModel("The user has been registrated, please, check your email to continue", "Success");
        }
    }
}
