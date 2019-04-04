using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

[assembly: InternalsVisibleTo("TransIT.BLL.Tests")]
namespace TransIT.BLL.Security.Hashers
{
    /// <summary>
    /// Implementation of IPasswordHasher.
    /// Class is internal for the assembly.
    /// </summary>
    internal class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// <see cref="IPasswordHasher.HashPassword(string)"/>
        /// </summary>
        /// <note>This method will return different results for the same password</note>
        public string HashPassword(string password)
        {
            var salt = GenerateSalt(16);
            var bytes = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
        }

        /// <summary>
        /// <see cref="IPasswordHasher.CheckMatch(string, string)"/>
        /// </summary>
        public bool CheckMatch(string password, string hashedPassword)
        {
            try
            {
                var parts = hashedPassword.Split(':');
                var salt = Convert.FromBase64String(parts[0]);
                var bytes = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generates salt for hashing algorithm
        /// </summary>
        /// <param name="length">Number of bytes in salt</param>
        /// <returns>Byte array of generated salt</returns>
        private static byte[] GenerateSalt(int length)
        {
            var salt = new byte[length];
            using(var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }
    }
}
