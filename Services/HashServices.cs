using System;
using System.Security.Cryptography;

namespace Services
{
    public class HashServices
    {
        public static (string salt, string passwordHash) Encrypt(string password)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[5];
            rng.GetBytes(buff);
            var salt = Convert.ToBase64String(buff);
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            var hashAlgorithm = SHA256.Create();
            var passwordHash = hashAlgorithm.ComputeHash(bytes);
            return (salt, BitConverter.ToString(passwordHash).Replace("-", ""));
        }

        public static string Decrypt(string passwordSalt, string password)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
            var hashAlgorithm = SHA256.Create();
            var passwordHash = hashAlgorithm.ComputeHash(bytes);
            return BitConverter.ToString(passwordHash).Replace("-", "");
        }
    }
}
