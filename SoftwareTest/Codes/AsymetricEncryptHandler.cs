using System.Security.Cryptography;

namespace SoftwareTest.Codes;

public class AsymetricEncryptHandler
{
    private string _privateKey;
    private string _publicKey;
    public AsymetricEncryptHandler()
    {
        using(RSA rsa = RSA.Create())
        {
            //pravate key
            RSAParameters PrivateKeyParams = rsa.ExportParameters(true);
            _privateKey = rsa.ToXmlString(true);

            //public key
            RSAParameters publicKeyParams = rsa.ExportParameters(false);
            RSAParameters publicKey = new RSAParameters
            {
                Modulus = publicKeyParams.Modulus,
                Exponent = publicKeyParams.Exponent
            };
            _publicKey = rsa.ToXmlString(false);
        }
    }

    public string Encrypt(string textToEncrypt)
    {
        string encrypted = AsymtricEncrypter.Encrypt(textToEncrypt, _publicKey);
        return encrypted;
    }

    public string Decrypt(string textToDecrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(_privateKey);
            byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
            byte[] byteArrayDecryptedValue = rsa.Decrypt(byteArrayTextToDecrypt, true);
            var decryptedDataAsString = System.Text.Encoding.UTF8.GetString(byteArrayDecryptedValue);

            return decryptedDataAsString;
        }
    }
}