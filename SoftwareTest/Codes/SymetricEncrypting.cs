using Microsoft.AspNetCore.DataProtection;

namespace SoftwareTest.Codes
{
    public class SymetricEncrypting
    {
        private readonly IDataProtector _protector;
        public SymetricEncrypting(IDataProtectionProvider protector)
        {
            _protector = protector.CreateProtector("EmilErSortDaHanIkkeErHvid");
        }

        public string EncryptSymetric(string textToEncrypt) =>
            _protector.Protect(textToEncrypt);
        
        public string DecryptSymetric(string textToDecrypt) =>
            _protector.Unprotect(textToDecrypt);
        
    }
}
