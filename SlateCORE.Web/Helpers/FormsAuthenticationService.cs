using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using SlateCORE.Web.Interfaces;

namespace SlateCORE.Web.Helpers
{
    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SetAuthCookie(string login, bool createPersistentCookie)
        {
            FormsAuthentication.SetAuthCookie(login, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}