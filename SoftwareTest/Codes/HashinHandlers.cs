using BCrypt.Net;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SoftwareTest.Codes
{
    public class HashinHandlers
    {
        public string HMACHasing(string textToHash)
        {
            string certificatePath = @"C:\Users\dhart\.aspnet\https\salt.cer";
            X509Certificate2 cert = new X509Certificate2(certificatePath);
            string key = cert.Thumbprint;

            byte[] byteArray = Encoding.ASCII.GetBytes(textToHash);
            byte[] myKey = Encoding.ASCII.GetBytes(key);

            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = myKey;

            byte[] hashedValue = hmac.ComputeHash(byteArray);
            return Convert.ToBase64String(hashedValue);
        }

        public bool HMACVerify(string input, string hashedCprFromDatabase)
        {
            string hashedInput = HMACHasing(input);

            // Compare the hashed input with the stored hash in the database
            return hashedInput.Equals(hashedCprFromDatabase);
        }
    }
}
