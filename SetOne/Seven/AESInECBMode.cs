using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MatasantoCrypto.Set1.One;

namespace MatasantoCrypto.Set1.Seven
{
    // NOTE on the Crypto providers from MSDN:
    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.aesmanaged(v=vs.100).aspx
    // Managed versus CSP performance
    // For small byte arrays, AesManaged is around twice as fast as AesCryptoServiceProvider.
    // For large arrays, it flips and AesCryptoServiceProvider is about twice as fast. Break-even is around 512 bytes.
    // Ed Brey 6/18/2010


    public interface IDecryptAESInECBMode
    {
        string Decrypt(string fileName, string keyString);
    }

    public class DecryptAESInECBMode : IDecryptAESInECBMode
    {
        private readonly IConvertHex _convertor;

        public DecryptAESInECBMode(IConvertHex convertor)
        {
            _convertor = convertor;
        }

        public string Decrypt(string fileName, string keyString)
        {
            byte[] fileContentBytes = LoadFileToByteArray(fileName);
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);

            return DecryptAES(fileContentBytes, keyBytes, CipherMode.ECB);
        }

        private string DecryptAES(byte[] fileContentBytes, byte[] keyBytes, CipherMode mode)
        {
            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (var aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = keyBytes;
                aesAlg.Mode = mode;

                // Create a decryptor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(fileContentBytes))
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(csDecrypt))
                {
                    // Read the decrypted bytes from the decrypting stream and place them in a string.
                    plaintext = srDecrypt.ReadToEnd();
                }
            }

            return plaintext;
        }


        private byte[] LoadFileToByteArray(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var asOneLine = String.Join(String.Empty, lines);

            return _convertor.GetByteArrayFromBase64(asOneLine);
        }

    }
}
