using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SlateCore.Security;

namespace SlateCORE.Web.Helpers
{
    public static class AccountService
    {
        public static string CreatePasswordHash(string password, string dynamicSalt)
        {
            string globalSalt = null;              //add reading from file
            var passwordHash = PasswordHash.CreatePasswordHash(password, dynamicSalt, globalSalt);

            return passwordHash;
        }
    }
}