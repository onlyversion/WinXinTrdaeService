using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web.Security;

namespace JinTong.Jyrj.Common
{
    public class EncryptHelper
    {

        //对称加密解密密钥(保证金软件加密密钥)
        private static string key = "FF123764";
        //默认密钥向量(保证金软件加密密钥向量)
        private static byte[] keys = { 0x24, 0x46, 0x57, 0x76, 0x9F, 0xB1, 0xCC, 0xF1 };

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="code">需加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string DesEncrypt(string code)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyIV = keys;
            byte[] inputBytes = Encoding.UTF8.GetBytes(code);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="code">加密字符串</param>
        /// <returns>解密后字符串</returns>
        public static string DesDecrypts(string code)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyIV = keys;
            byte[] inputBytes = Convert.FromBase64String(code);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cs.Write(inputBytes, 0, inputBytes.Length);
            cs.FlushFinalBlock();
            return Encoding.UTF8.GetString(ms.ToArray());
        }

        /// <summary>
        /// 数据加密 0:SHA 1:MD5
        /// </summary>
        /// <param name="original"></param>
        /// <param name="encryptFormat"></param>
        /// <returns></returns>
        public static string Encrypt(string original, int encryptFormat)
        {
            string ciphertext = "";
            switch (encryptFormat)
            {
                case 0:
                    ciphertext = FormsAuthentication.HashPasswordForStoringInConfigFile(original, "SHA1");
                    break;
                case 1:
                    ciphertext = FormsAuthentication.HashPasswordForStoringInConfigFile(original, "MD5");
                    break;
            }
            return ciphertext;
        }


        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string EncryptStrByHash(string src)
        {
            if (src.Length == 0)
            {
                return "";
            }
            byte[] HaKey = System.Text.Encoding.ASCII.GetBytes((src + "jindou").ToCharArray());
            byte[] HaData = new byte[20];
            HMACSHA1 Hmac = new HMACSHA1(HaKey);
            CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
            try
            {
                cs.Write(HaData, 0, HaData.Length);
            }
            finally
            {
                cs.Close();
            }
            string HaResult = System.Convert.ToBase64String(Hmac.Hash).Substring(0, 16);
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(HaResult.ToCharArray());
            byte[] RiDataBuf = System.Text.Encoding.ASCII.GetBytes(src.ToCharArray());
            byte[] EncodedBytes = { };
            MemoryStream ms = new MemoryStream();
            RijndaelManaged rv = new RijndaelManaged();
            cs = new CryptoStream(ms, rv.CreateEncryptor(RiKey, RiKey), CryptoStreamMode.Write);
            try
            {
                cs.Write(RiDataBuf, 0, RiDataBuf.Length);
                cs.FlushFinalBlock();
                EncodedBytes = ms.ToArray();
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            return HaResult + System.Convert.ToBase64String(EncodedBytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DecrypStrByHash(string src)
        {
            if (src.Length < 40) return "";
            byte[] SrcBytes = System.Convert.FromBase64String(src.Substring(16));
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(src.Substring(0, 16).ToCharArray());
            byte[] InitialText = new byte[SrcBytes.Length];
            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(SrcBytes);
            CryptoStream cs = new CryptoStream(ms, rv.CreateDecryptor(RiKey, RiKey), CryptoStreamMode.Read);
            try
            {
                cs.Read(InitialText, 0, InitialText.Length);
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            for (int i = 0; i < InitialText.Length; ++i) if (InitialText[i] > 0) Result.Append((char)InitialText[i]);
            return Result.ToString();
        }
    }
}
