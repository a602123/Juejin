using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class AESHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="key">密钥（16位 24位 32位）</param>
        /// <param name="iv">向量（16位）</param>
        /// <returns>加密后的base64串</returns>
        public static string Encrypt(string source, string key, string iv)
        {

            var bytes = Encoding.UTF8.GetBytes(source);
            using (var aesProvider = Aes.Create())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.IV = Encoding.UTF8.GetBytes(iv);
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, aesProvider.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }

            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="source">加密后的base64串</param>
        /// <param name="key">密钥（16位 24位 32位）</param>
        /// <param name="iv">向量（16位）</param>
        /// <returns></returns>
        public static string Decrypt(string source, string key, string iv)
        {
            var bytes = Convert.FromBase64String(source);
            using (var aesProvider = Aes.Create())
            {
                aesProvider.Key = Encoding.UTF8.GetBytes(key);
                aesProvider.IV = Encoding.UTF8.GetBytes(iv);
                var decryptor = aesProvider.CreateDecryptor(aesProvider.Key, aesProvider.IV);
                using (var msDecrypt = new MemoryStream(bytes))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            var res = srDecrypt.ReadToEnd();
                            return res;
                        }
                    }
                }
            }

        }
    }
}
