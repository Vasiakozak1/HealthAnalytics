namespace HealthAnalytics.BusinessLogic.Services.Abstract
{
    public interface IHashingService
    {
        string GetHash(string input);
        bool VerifyHash(string textToVerify, string expectedHash);
    }
}
