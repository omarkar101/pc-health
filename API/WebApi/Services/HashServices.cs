using System;
using System.Security.Cryptography;

namespace WebApi.Services
{
    public class HashServices
    {
        public static (string salt, string passwordHash) Encrypt(string password)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[5];
            rng.GetBytes(buff);
            string salt = Convert.ToBase64String(buff);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            var hashAlgorithm = SHA256.Create();
            byte[] passwordhash = hashAlgorithm.ComputeHash(bytes);
            return (salt, BitConverter.ToString(passwordhash).Replace("-", ""));
        }

        public static string Decrypt(string passwordSalt, string password)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
            var hashAlgorithm = SHA256.Create();
            byte[] passwordhash = hashAlgorithm.ComputeHash(bytes);
            return BitConverter.ToString(passwordhash).Replace("-", "");
        }
    }
}
