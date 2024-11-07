using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;

namespace QLDT_WPF.Services
{
    public class SecurityService
    {
        // Variables
        private string _encryptionKey; 
        private string _hashKey;
        private string _algorithmEncryption;
        private string _algorithmHashing;
        
        // Constructors
        public SecurityService()
        {
            this.GetValue();
        }

        // Init get value
        private void GetValue() {
            _encryptionKey = ConfigurationManager.AppSettings["Security:EncryptionKey"];
            _hashKey = ConfigurationManager.AppSettings["Security:HashKey"];
            _algorithmEncryption = ConfigurationManager.AppSettings["Security:AlgorithmEncryption"];
            _algorithmHashing = ConfigurationManager.AppSettings["Security:AlgorithmHashing"];
        }

        // Method to encrypt a string
        public string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aesAlg.IV = new byte[16]; // For simplicity, assuming IV is zeroed, but it should be dynamic in real-world usage

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        // Method to decrypt a string
        public string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(_encryptionKey);
                aesAlg.IV = new byte[16]; // Should be the same IV used during encryption

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        // Method to hash a string using HMACSHA256
        public string Hash(string input)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_hashKey)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashValue);
            }
        }

        // Method to validate a hash
        public bool ValidateHash(string input, string hash)
        {
            string computedHash = Hash(input);
            return computedHash == hash;
        }
    }
}