﻿using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ButodoProject.Core.Helper
{
    public class CryptoHelper
    {
        public static string EncryptByMd5(string text)
        {
            var md5 = new MD5CryptoServiceProvider();

            var btr = Encoding.UTF8.GetBytes(text);
            btr = md5.ComputeHash(btr);

            var sb = new StringBuilder();

            foreach (var ba in btr)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        private static readonly string Password = ConfigurationManager.AppSettings["CryptoPassword"];

        public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            var ms = new MemoryStream();

            var alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            var cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);

            cs.Write(clearData, 0, clearData.Length);

            cs.Close();

            var encryptedData = ms.ToArray();

            return encryptedData;
        }

        public static string Encrypt(string clearText)
        {
            var clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);

            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

            return Convert.ToBase64String(encryptedData);

        }

        public static byte[] Encrypt(byte[] clearData)
        {
            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));

        }

        public static void Encrypt(string fileIn, string fileOut)
        {

            var fsIn = new FileStream(fileIn,
                FileMode.Open, FileAccess.Read);
            var fsOut = new FileStream(fileOut,
                FileMode.OpenOrCreate, FileAccess.Write);

            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var alg = Rijndael.Create();
            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            var cs = new CryptoStream(fsOut,
                alg.CreateEncryptor(), CryptoStreamMode.Write);

            const int bufferLen = 4096;
            var buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                cs.Write(buffer, 0, bytesRead);
            } while (bytesRead != 0);

            cs.Close();
            fsIn.Close();
        }

        public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            var ms = new MemoryStream();

            var alg = Rijndael.Create();

            alg.Key = Key;
            alg.IV = IV;

            var cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(cipherData, 0, cipherData.Length);

            cs.Close();

            var decryptedData = ms.ToArray();

            return decryptedData;
        }

        public static string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);

            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var decryptedData = Decrypt(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static string DecryptForLicence(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);

            var pdb = new PasswordDeriveBytes("B1w3bSe*LiCenc3", new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
                0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            var decryptedData = Decrypt(cipherBytes,
                pdb.GetBytes(32), pdb.GetBytes(16));

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }

        public static byte[] Decrypt(byte[] cipherData)
        {
            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }

        public static void Decrypt(string fileIn, string fileOut)
        {

            var fsIn = new FileStream(fileIn,
                FileMode.Open, FileAccess.Read);
            var fsOut = new FileStream(fileOut,
                FileMode.OpenOrCreate, FileAccess.Write);

            var pdb = new PasswordDeriveBytes(Password, new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
                0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            var alg = Rijndael.Create();

            alg.Key = pdb.GetBytes(32);
            alg.IV = pdb.GetBytes(16);

            var cs = new CryptoStream(fsOut, alg.CreateDecryptor(), CryptoStreamMode.Write);

            const int bufferLen = 4096;
            var buffer = new byte[bufferLen];
            int bytesRead;

            do
            {
                bytesRead = fsIn.Read(buffer, 0, bufferLen);

                cs.Write(buffer, 0, bytesRead);

            } while (bytesRead != 0);

            cs.Close();
            fsIn.Close();
        }
    }
}