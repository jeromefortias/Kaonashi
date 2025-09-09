namespace Nexai.Kaonashi.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public static class CryptoMgt
    {
        public static string SHA256HashFile(string fullName)
        {
            using (SHA256 SHA256 = SHA256Managed.Create())
            {
                using (FileStream fileStream = File.OpenRead(fullName))
                    return Convert.ToBase64String(SHA256.ComputeHash(fileStream));
            }
        }
        /// <summary>
        /// Hash a string
        /// </summary>
        /// <param name="text">a text string</param>
        /// <returns>A string with the hash content</returns>
        public static string SHA256HashString(string text)
        {
            try
            {
                SHA256 hash = SHA256.Create();
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder sha256signature = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sha256signature.Append(bytes[i].ToString("x2"));
                }
                return sha256signature.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string AES256EncryptText(string text, string password)
        {
            string _password = "abcdefghijklmnopqrstuvwx";

            password = (password + _password).Substring(0, 24);
            try
            {
                var myKey = Encoding.UTF8.GetBytes(password);
                using (var AES = Aes.Create())
                {
                    using (var encryptor = AES.CreateEncryptor(myKey, AES.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(text);
                            }

                            var myAESIv = AES.IV;
                            var decryptedText = msEncrypt.ToArray();
                            var result = new byte[myAESIv.Length + decryptedText.Length];

                            Buffer.BlockCopy(myAESIv, 0, result, 0, myAESIv.Length);
                            Buffer.BlockCopy(decryptedText, 0, result, myAESIv.Length, decryptedText.Length);
                            return Convert.ToBase64String(result);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string AES256TextDecrypt(string text, string password)
        {
            string _password = "abcdefghijklmnopqrstuvwx";
            password = (password + _password).Substring(0, 24);
            try
            {
                var CryptedText = Convert.FromBase64String(text);
                var myAESIv = new byte[16];
                var myCipher = new byte[16];
                Buffer.BlockCopy(CryptedText, 0, myAESIv, 0, myAESIv.Length);
                Buffer.BlockCopy(CryptedText, myAESIv.Length, myCipher, 0, myAESIv.Length);
                var key = Encoding.UTF8.GetBytes(password);
                using (var AES = Aes.Create())
                {
                    using (var decryptor = AES.CreateDecryptor(key, myAESIv))
                    {
                        string result;
                        using (var msDecrypt = new MemoryStream(myCipher))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
