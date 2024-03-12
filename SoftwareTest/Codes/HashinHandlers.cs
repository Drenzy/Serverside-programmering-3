// HashinHandlers.cs
using BCrypt.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SoftwareTest.Codes
{
    public class HashinHandlers
    {
        public string BcryptHash(string textToHash)
        {
            // Adjust the parameters as needed
            return BCrypt.Net.BCrypt.HashPassword(textToHash, 10, true);
        }

        public bool BcryptVerify(string textToHash, string hashedValue)
        {
            return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue, true);
        }

        public string HMACHasing(string textToHas)
        {
            string certificatePath = @"C:\Users\dhart\.aspnet\https\salt.cer";
            X509Certificate2 cert = new X509Certificate2(certificatePath);
            string key = cert.Thumbprint;

            byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
            byte[] myKey = Encoding.ASCII.GetBytes(key);

            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = myKey;

            byte[] hashedValue = hmac.ComputeHash(byteArray);
            return Convert.ToBase64String(hashedValue);

        }
    }
}
