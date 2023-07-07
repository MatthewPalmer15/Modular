using System.Security.Cryptography;
using System.Text;

namespace Modular.Core
{
    public static class Encryption
    {

        private static readonly string EncryptionMethod = SystemConfig.Load("EncryptionMethod").Value;

        private static readonly string EncryptionKey = SystemConfig.Load("EncryptionKey").Value;

        private static readonly string EncryptionIV = SystemConfig.Load("EncryptionIV").Value;

        /// <summary>
        /// Encrypts the input string, and returns an encrypted string
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            SymmetricAlgorithm? Algorithm = SymmetricAlgorithm.Create(EncryptionMethod);
            if (Algorithm != null)
            {
                UTF8Encoding utfEncoding = new UTF8Encoding();

                Algorithm.Key = Convert.FromBase64String(EncryptionKey);
                Algorithm.IV = Convert.FromBase64String(EncryptionIV);

                Byte[] InputData = utfEncoding.GetBytes(Text);
                Byte[] OutputData = Transform(InputData, Algorithm.CreateEncryptor());

                return Convert.ToBase64String(OutputData);
            }
            else
            {
                throw new ModularException(ExceptionType.EncryptionError, "Algorithm is not recognisable.");
            }
        }

        public static string Decrypt(string Text)
        {
            SymmetricAlgorithm? Algorithm = SymmetricAlgorithm.Create(EncryptionMethod);
            if (Algorithm != null)
            {
                Algorithm.Key = Convert.FromBase64String(EncryptionKey);
                Algorithm.IV = Convert.FromBase64String(EncryptionIV);

                byte[] InputData = Convert.FromBase64String(Text);
                byte[] OutputData = Transform(InputData, Algorithm.CreateDecryptor());

                return Convert.ToBase64String(OutputData);
            }
            else
            {
                return string.Empty;
            }
        }

        private static Byte[] Transform(byte[] InputData, ICryptoTransform CryptoTransform)
        {
            MemoryStream MemoryStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(MemoryStream, CryptoTransform, CryptoStreamMode.Write);

            cryptoStream.Write(InputData, 0, InputData.Length);
            cryptoStream.FlushFinalBlock();


            MemoryStream.Position = 0;
            byte[] result = new byte[(int)MemoryStream.Length];
            MemoryStream.Read(result, 0, result.Length);

            MemoryStream.Close();
            cryptoStream.Close();

            return result;
        }

    }
}
