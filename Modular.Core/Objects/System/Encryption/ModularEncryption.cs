using Modular.Core.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Modular.Core.Security
{
    public static class Encryption
    {

        #region "  Properties  "

        private readonly static string DefaultEncryptionKey = "0U278TrMD5VT1r7pKTgURYMBbEwyhQrdpApxxHPyOys=";

        private static string EncryptionKey
        {
            get
            {
                string ConfigValue = AppConfig.GetValue("Encryption:EncryptionKey");
                return !string.IsNullOrEmpty(ConfigValue) ? ConfigValue : DefaultEncryptionKey;
            }
        }

        #endregion

        #region "  Methods  "

        /// <summary>
        /// Encrypt a string value
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string Encrypt(string Value)
        {
            byte[] EncryptedBytes;

            byte[] EncryptedResult;

            using (Aes AESEncryption = Aes.Create())
            {
                AESEncryption.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                AESEncryption.GenerateIV();

                ICryptoTransform AESEncryptor = AESEncryption.CreateEncryptor(AESEncryption.Key, AESEncryption.IV);

                using (MemoryStream MemoryStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, AESEncryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter StreamWriter = new StreamWriter(CryptoStream))
                        {
                            StreamWriter.Write(Value);
                        }

                        EncryptedBytes = MemoryStream.ToArray();
                    }
                }

                // Combine IV and encrypted data
                EncryptedResult = new byte[AESEncryption.IV.Length + EncryptedBytes.Length];
                Buffer.BlockCopy(AESEncryption.IV, 0, EncryptedResult, 0, AESEncryption.IV.Length);
                Buffer.BlockCopy(EncryptedBytes, 0, EncryptedResult, AESEncryption.IV.Length, EncryptedBytes.Length);
            }

            return Convert.ToBase64String(EncryptedResult);
        }

        /// <summary>
        /// Decrypt a string value
        /// </summary>
        /// <param name="EncryptedValue"></param>
        /// <returns></returns>
        public static string Decrypt(string EncryptedValue)
        {
            byte[] EncryptedBytes = Convert.FromBase64String(EncryptedValue);

            using (Aes AESEncryption = Aes.Create())
            {
                AESEncryption.Key = Encoding.UTF8.GetBytes(EncryptionKey);
                byte[] EncryptionIV = new byte[AESEncryption.BlockSize / 8];
                byte[] EncryptedData = new byte[EncryptedBytes.Length - EncryptionIV.Length];

                Buffer.BlockCopy(EncryptedBytes, 0, EncryptionIV, 0, EncryptionIV.Length);
                Buffer.BlockCopy(EncryptedBytes, EncryptionIV.Length, EncryptedData, 0, EncryptedData.Length);

                AESEncryption.IV = EncryptionIV;

                ICryptoTransform AESDecryptor = AESEncryption.CreateDecryptor(AESEncryption.Key, AESEncryption.IV);

                using (MemoryStream MemoryStream = new MemoryStream(EncryptedData))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemoryStream, AESDecryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader StreamReader = new StreamReader(CryptoStream))
                        {
                            return StreamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion

    }
}
