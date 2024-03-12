using System.Security.Cryptography;
using System.Text;
namespace SoftwareTest.Codes
{
    public class HashinHandlers
    {
        public string MD5Hasing(string textToHas)
        {
            MD5 md5 = MD5.Create();
            byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
            byte[] hasedValue = md5.ComputeHash(byteArray);

            Convert.ToBase64String(hasedValue);
            return "";
        } 
        
        public string SHAHash(string textToHas)
        {
            SHA256 sha = SHA256.Create();
            byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
            byte[] hasedValue = sha.ComputeHash(byteArray);

            return Convert.ToBase64String(hasedValue);
        }
        
        public string HMACHash(string textToHas)
        {
            
            byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
            byte[] myKey = Encoding.ASCII.GetBytes("EmilEpstien");

            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = myKey;

           byte[] hasedValue = hmac.ComputeHash(byteArray);
            return Convert.ToBase64String(hasedValue);
        }

        public string PBKDF2Hash(string textToHas)
        {

            byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
            byte[] byteArraySalt = Encoding.ASCII.GetBytes("DanielHartwich");

            var hashinAlgo = new HashAlgorithmName("SHA256");
            int itiration = 10;

            byte[] hashedalue = Rfc2898DeriveBytes.Pbkdf2(byteArray, byteArraySalt, itiration,hashinAlgo, 32);

            return Convert.ToBase64String(hashedalue);
            
        }

        public string BCRYPTHash(string textToHas)
        {
            //return BCrypt.Net.BCrypt.HashPassword(textToHas);
            //string salt = BCrypt.Net.BCrypt.GenerateSalt();
            //return BCrypt.Net.BCrypt.HashPassword(textToHas, salt, true, BCrypt.Net.HashType.SHA256);

            return BCrypt.Net.BCrypt.HashPassword(textToHas, 10, true);
        }

        public bool BCRYPTVerify(string textToHas, string hashedValue)
        {
            //return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue);
            //return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue, true, BCrypt.Net.HashType.SHA256);

            return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue, true);
        }
    }
}
