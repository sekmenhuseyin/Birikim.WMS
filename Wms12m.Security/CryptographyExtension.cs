using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Wms12m.Security
{
    /// <summary>
    /// Bu Class 128 karakterlik bir şifreleme yapar.
    /// </summary>
    public static class CryptographyExtension
    {
        /// Bu Anahtarı Dilediğiniz Gibi Değiştirebilirsiniz (MFB)
        const string VARSAYILAN_ANAHTAR = "Birikim12*";
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        const int KEYSIZE = 256;
        // This constant determines the number of iterations for the password bytes generation function.
        const int DERIVATION_ITERATIONS = 1000;
        /// <summary>
        /// İlk aldığı parametreyi şifreleyip şifreli text değeri döner.
        /// <para>İkinci parametre şifrelemenin anahtarıdır. Bu değere göre şifreleme olur.</para>
        /// </summary>
        public static string Sifrele(string sifrelenecekText, string sifreAnahtari = VARSAYILAN_ANAHTAR)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(sifrelenecekText);
            using (var password = new Rfc2898DeriveBytes(sifreAnahtari, saltStringBytes, DERIVATION_ITERATIONS))
            {
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// İlk parametre şifrelenmiş değerdir. Bu değere karşılık şifresiz text ifade geri döner. 
        /// <para>İkinci parametre bu şifreyi çözmek için kullanılacak anahtar değerdir.</para>
        /// </summary>
        public static string Cozumle(string cozulecekSifreliText, string sifreAnahtari = VARSAYILAN_ANAHTAR)
        {
            if (string.IsNullOrEmpty(cozulecekSifreliText))
                return "";
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cozulecekSifreliText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(KEYSIZE / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(KEYSIZE / 8).Take(KEYSIZE / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((KEYSIZE / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((KEYSIZE / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(sifreAnahtari, saltStringBytes, DERIVATION_ITERATIONS))
            {
                var keyBytes = password.GetBytes(KEYSIZE / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
        static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);


            return randomBytes;
        }
    }
}