using HealthAnalytics.BusinessLogic.Services.Abstract;
using System.Security.Cryptography;
using System.Text;

namespace HealthAnalytics.BusinessLogic.Services.Implementation
{
    public class Md5HashingService : IHashingService
    {
        private MD5 hasingAlgorithm;

        public Md5HashingService()
        {
            hasingAlgorithm = MD5.Create();
        }

        public string GetHash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = hasingAlgorithm.ComputeHash(inputBytes);
            var hashStrBuilder = new StringBuilder();

            foreach (var hashedByte in hashedBytes)
            {
                hashStrBuilder.Append(hashedByte.ToString("x2"));
            }

            return hashStrBuilder.ToString();
        }

        public bool VerifyHash(string textToVerify, string expectedHash)
        {
            string hashToVerify = GetHash(textToVerify);

            return hashToVerify.Equals(expectedHash);
        }
    }
}
