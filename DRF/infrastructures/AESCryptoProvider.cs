using System.Security.Cryptography;
using System.Text;

namespace DRF.infrastructures
{
    public class AESCryptoProvider
    {
        private AesCryptoServiceProvider crypt;
        private AESCryptoProvider()
        {
            byte[] key = { 100, 145, 58, 215, 189, 32, 78, 75, 71, 208, 72, 52, 31, 73, 253, 14, 243, 111, 179, 210, 25, 55, 225, 205, 47, 60, 157, 43, 217, 119, 112, 3 };
            byte[] iv = { 191, 235, 26, 236, 111, 71, 53, 120, 15, 217, 211, 247, 168, 160, 241, 29 };

            crypt = new AesCryptoServiceProvider();
            crypt.BlockSize = 128;
            crypt.KeySize = 256;
            crypt.Key = key;
            crypt.IV = iv;
            crypt.Mode = CipherMode.CBC;
            crypt.Padding = PaddingMode.PKCS7;


        }
        private static AESCryptoProvider _instance;
        public static AESCryptoProvider GetInstnace
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AESCryptoProvider();
                }
                return _instance;
            }
        }
        public string Encrypt(string clearText)
        {
            ICryptoTransform transform = crypt.CreateEncryptor();
            byte[] encryptedBytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(clearText), 0, clearText.Length);
            string str = Convert.ToBase64String(encryptedBytes);
            return str;
        }
        public string Decrypt(string chiperText)
        {
            ICryptoTransform transform = crypt.CreateDecryptor();
            byte[] encBytes = Convert.FromBase64String(chiperText);
            byte[] decryptBytes = transform.TransformFinalBlock(encBytes, 0, encBytes.Length);
            string str = ASCIIEncoding.ASCII.GetString(decryptBytes);
            return str;
        }
    }
}
