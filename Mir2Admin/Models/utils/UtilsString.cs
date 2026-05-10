using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Mir2Admin.Models
{
    public class CUtilsString
    {
        public CUtilsString()
        {
        }
        public static string GetBankTitle(int iBankIndex)
        {
            string bankTitle = "";
//             switch (iBankIndex)
//             {
//             }
            return bankTitle;
        }
        public static DataTable BindBankDropDownList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");

            List<string> bankList = new List<string>();

            for (int i = 0; i < bankList.Count; i++) dt.Rows.Add(new object[] { bankList[i], i });
            return dt;
        }

        public static string EncryptStr(string text)
        {
            string PasswordKey = "forever21";
            byte[] inputText = System.Text.Encoding.Unicode.GetBytes(text);
            byte[] passwordSalt = Encoding.ASCII.GetBytes(PasswordKey.Length.ToString());
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(PasswordKey, passwordSalt);

            Rijndael rijAlg = Rijndael.Create();
            rijAlg.Key = secretKey.GetBytes(32);
            rijAlg.IV = secretKey.GetBytes(16);

            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(inputText, 0, inputText.Length);
            csEncrypt.FlushFinalBlock();
            byte[] encryptBytes = msEncrypt.ToArray();
            msEncrypt.Close();
            csEncrypt.Close();

            string encryptedData = Convert.ToBase64String(encryptBytes);
            return encryptedData;
        }
        public static string DecryptStr(string text)
        {
            string PasswordKey = "forever21";
            byte[] encryptedData = Convert.FromBase64String(text);
            byte[] passwordSalt = Encoding.ASCII.GetBytes(PasswordKey.Length.ToString());
            PasswordDeriveBytes secretKey = new PasswordDeriveBytes(PasswordKey, passwordSalt);

            Rijndael rijAlg = Rijndael.Create();
            rijAlg.Key = secretKey.GetBytes(32);
            rijAlg.IV = secretKey.GetBytes(16);
            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
            MemoryStream msDecrypt = new MemoryStream(encryptedData);
            CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            int decryptedCount = csDecrypt.Read(encryptedData, 0, encryptedData.Length);
            msDecrypt.Close();
            csDecrypt.Close();

            string decryptedData = Encoding.Unicode.GetString(encryptedData, 0, decryptedCount);
            return decryptedData;
        }
        public static string GetMoneyComma(int money)
        {
            return string.Format("{0:n0}", money);
        }
        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\n" + logMessage);
                //txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                //    DateTime.Now.ToLongDateString());
                //txtWriter.WriteLine("  :");
                //txtWriter.WriteLine("  :{0}", logMessage);
                //txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                txtWriter.WriteLine("log error: {0}!", ex.ToString());
            }
        }

    }
}
