using HealthAnalytics.BusinessLogic.Data.ViewModels;

namespace HealthAnalytics.BusinessLogic
{
    public static class Constants
    {
        public const double USER_REGISTER_TOKEN_EXPIRE_HOURS = 3;
        public const string TEMPLATES_FOLDER_NAME = "Templates";
        public static MessageViewModel GetSuccessfullRegistrationMessage()
        {
            return new MessageViewModel("The user has been registrated, please, check your email to continue", "Success");
        }
    }
}
