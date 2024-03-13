using System.Security.Cryptography;

namespace SoftwareTest.Codes
{
    public class AsymtricEncrypter
    {
        public static string Encrypt(string textToEncrypt, string _publicKey)
        {
            using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_publicKey);
                byte[] byteArrayTextToEncrypt = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                byte[] byteArrayEncryptedValue = rsa.Encrypt(byteArrayTextToEncrypt, true);
                var encryptedDataAsString = System.Convert.ToBase64String(byteArrayEncryptedValue);
                
                return encryptedDataAsString;
            }
        }
    }
}
