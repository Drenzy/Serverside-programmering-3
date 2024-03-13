using BCrypt.Net;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SoftwareTest.Codes
{
    public enum ReturnTypes
    {
        String,
        ByteArray,
        Int,
        UTFString,
        HexString
    }
    public class HashingHandlers
    {
        public dynamic HMACHasing(string textToHash, ReturnTypes returnType)
        {
            string certificatePath = @"C:\Users\dhart\.aspnet\https\salt.cer";
            X509Certificate2 cert = new X509Certificate2(certificatePath);
            string key = cert.Thumbprint;

            byte[] byteArray = Encoding.ASCII.GetBytes(textToHash);
            byte[] myKey = Encoding.ASCII.GetBytes(key);

            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = myKey;

            byte[] hashedValue = hmac.ComputeHash(byteArray);
            return returnType switch
            {
                ReturnTypes.String => Convert.ToBase64String(hashedValue),
                ReturnTypes.ByteArray => hashedValue,
                ReturnTypes.Int => BitConverter.ToInt32(hashedValue, 0),
                ReturnTypes.HexString => BitConverter.ToString(hashedValue).Replace("-", ""),
                //Both these UTF strings return the same, and is therefore correct.
                ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue),
                _ => throw new ArgumentException("Invalid return type specified"),
            };
        }

        public bool HMACVerify(string input, string hashedCprFromDatabase, ReturnTypes returnType)
        {
            string hashedInput = HMACHasing(input, returnType);

            // Compare the hashed input with the stored hash in the database
            return hashedInput.Equals(hashedCprFromDatabase);
        }

        /*
 *                                   THE REST FROM HERE, WE DON'T USE ANYMORE
 */


        //public dynamic MD5Hasing(string textToHas, ReturnTypes returnType)
        //{
        //    MD5 md5 = MD5.Create();
        //    byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
        //    byte[] hashedValue = md5.ComputeHash(byteArray);

        //    return returnType switch
        //    {
        //        ReturnTypes.String => Convert.ToBase64String(hashedValue),
        //        ReturnTypes.ByteArray => hashedValue,
        //        ReturnTypes.Int => BitConverter.ToInt32(hashedValue, 0),
        //        ReturnTypes.HexString => BitConverter.ToString(hashedValue).Replace("-", ""),
        //        //Both these UTF strings return the same, and is therefore correct.
        //        ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue),
        //        //ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue, 0, hashedValue.Length),
        //        _ => throw new ArgumentException("Invalid return type specified"),
        //    };
        //}
        //public dynamic SHAHash(string textToHas, ReturnTypes returnType)
        //{
        //    SHA256 sha = SHA256.Create();
        //    byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
        //    byte[] hashedValue = sha.ComputeHash(byteArray);

        //    return returnType switch
        //    {
        //        ReturnTypes.String => Convert.ToBase64String(hashedValue),
        //        ReturnTypes.ByteArray => hashedValue,
        //        ReturnTypes.Int => BitConverter.ToInt32(hashedValue, 0),
        //        ReturnTypes.HexString => BitConverter.ToString(hashedValue).Replace("-", ""),
        //        //Both these UTF strings return the same, and is therefore correct.
        //        ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue),
        //        //ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue, 0, hashedValue.Length),
        //        _ => throw new ArgumentException("Invalid return type specified"),
        //    };
        //}

        //public dynamic PBKDF2Hash(string textToHas, ReturnTypes returnType)
        //{

        //    byte[] byteArray = Encoding.ASCII.GetBytes(textToHas);
        //    byte[] byteArraySalt = Encoding.ASCII.GetBytes("DanielHartwich");

        //    var hashingAlgo = new HashAlgorithmName("SHA256");
        //    int itiration = 10;

        //    byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(byteArray, byteArraySalt, itiration, hashingAlgo, 32);

        //    return returnType switch
        //    {
        //        ReturnTypes.String => Convert.ToBase64String(hashedValue),
        //        ReturnTypes.ByteArray => hashedValue,
        //        ReturnTypes.Int => BitConverter.ToInt32(hashedValue, 0),
        //        ReturnTypes.HexString => BitConverter.ToString(hashedValue).Replace("-", ""),
        //        //Both these UTF strings return the same, and is therefore correct.
        //        ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue),
        //        //ReturnTypes.UTFString => Encoding.UTF8.GetString(hashedValue, 0, hashedValue.Length),
        //        _ => throw new ArgumentException("Invalid return type specified"),
        //    };

        //}
        //public dynamic BCRYPTHash(string textToHas)
        //{
        //    //return BCrypt.Net.BCrypt.HashPassword(textToHas);
        //    string salt = BCrypt.Net.BCrypt.GenerateSalt();
        //    return BCrypt.Net.BCrypt.HashPassword(textToHas, salt, true, BCrypt.Net.HashType.SHA256);

        //    //return BCrypt.Net.BCrypt.HashPassword(textToHas, 10, true);
        //}
        //public bool BCRYPTVerify(string textToHas, string hashedValue)
        //{
        //    //return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue);
        //    return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue, true, BCrypt.Net.HashType.SHA256);

        //    //return BCrypt.Net.BCrypt.Verify(textToHas, hashedValue, true);
        //}
    }
}
