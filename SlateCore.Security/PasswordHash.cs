using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SlateCore.Security
{
    public static class PasswordHash
    {

        /// <summary>
        /// The method generates the dynamic salt for saving in DB.
        /// Each user gets individual dynamic salt.
        /// </summary>
        /// <returns>The dynamic salt</returns>
        public static string CreateDynamicSalt()
        {
            var random = new RNGCryptoServiceProvider();
            int maxLength = 32;                             //add the constant in a special file
            byte[] salt = new byte[maxLength];
            random.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }


        /// <summary>
        /// The method creates the hash of the password for saving in DB.
        /// Formula for hash: hash((hash(hash(password)+dynamic salt)+global salt)
        /// </summary>
        /// <param name="password">The password, which enter the user</param>
        /// <param name="dynamicSalt">The salt, which generated after sign up the user</param>
        /// <param name="globalSalt">The common salt for project</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password, string dynamicSalt, string globalSalt)
        {
            byte[] passwordInBytes = Encoding.UTF8.GetBytes(password);
            byte[] dynamicSaltInBytes = Encoding.UTF8.GetBytes(dynamicSalt);
            byte[] globalSaltInBytes = Encoding.UTF8.GetBytes(globalSalt);
            var sha = new SHA1CryptoServiceProvider();

            var hashOfPassword = sha.ComputeHash(passwordInBytes);

            var firstAddition = hashOfPassword.Concat(dynamicSaltInBytes).ToArray();
            var hashOfPasswordAndDS = sha.ComputeHash(firstAddition);

            var secondAddition = hashOfPasswordAndDS.Concat(globalSaltInBytes).ToArray();
            var passwordHash = sha.ComputeHash(secondAddition).ToString();

            return passwordHash;   
        }

    }
}