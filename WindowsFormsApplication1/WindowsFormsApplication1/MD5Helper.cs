using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class MD5Helper
    {
        /// <summary>
        /// 计算MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeMD5Hash(string text)
        {
            var bytes = new UTF8Encoding().GetBytes(text);
            var hash = MD5.Create().ComputeHash(bytes);
            return BitConverter.ToString(hash).Replace("-", "");
        }

        /// <summary>
        /// 验证是否MD5
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public static bool IsValidMD5(string md5)
        {
            if (md5 == null || md5.Length != 32)
            {
                return false;
            }
            return md5.All(x => (x >= '0' && x <= '9') || (x >= 'a' && x <= 'f') || (x >= 'A' && x <= 'F'));

        }
    }
}
