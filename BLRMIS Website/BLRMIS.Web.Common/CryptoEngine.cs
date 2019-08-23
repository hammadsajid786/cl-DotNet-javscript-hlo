using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BLRMIS.Web.Common
{
    public class CryptoEngine
    {
        //Key = sblw-3hn8-sqoy19
        public static string Encrypt(string input,string key)
        {
            byte[] inputArray = UTF8Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            string base64String = Convert.ToBase64String(resultArray, 0, resultArray.Length);
            return HttpUtility.UrlEncode(base64String);
        }
        public static string Decrypt(string input, string key)
        {
            string base64String = HttpUtility.UrlDecode(input);
            byte[] inputArray = Convert.FromBase64String(base64String);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = UTF8Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
