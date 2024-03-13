using System;
using System.IO;
using System.Security.Cryptography;

namespace SoftwareTest.Codes
{
    public class AsymetricEncryptHandler
    {
        private string _privateKey;
        private string _publicKey;

        public AsymetricEncryptHandler()
        {
            try
            {
                if (!File.Exists("privateKey.xml") || !File.Exists("publicKey.xml"))
                {
                    using (RSA rsa = RSA.Create())
                    {
                        // Private key
                        RSAParameters privateKeyParams = rsa.ExportParameters(true);
                        _privateKey = rsa.ToXmlString(true);

                        // Public key
                        RSAParameters publicKeyParams = rsa.ExportParameters(false);
                        _publicKey = rsa.ToXmlString(false);

                        File.WriteAllText("privateKey.xml", _privateKey);
                        File.WriteAllText("publicKey.xml", _publicKey);
                    }
                }
                else
                {
                    _privateKey = File.ReadAllText("privateKey.xml");
                    _publicKey = File.ReadAllText("publicKey.xml");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during key generation/retrieval: {ex.Message}");
                throw;
            }
        }

        public string Encrypt(string textToEncrypt)
        {
            try
            {
                string encrypted = AsymtricEncrypter.Encrypt(textToEncrypt, _publicKey);
                return encrypted;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during encryption: {ex.Message}");
                throw;
            }
        }

        public string Decrypt(string textToDecrypt)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error during decryption: {ex.Message}");
                throw;
            }
        }
    }
}
