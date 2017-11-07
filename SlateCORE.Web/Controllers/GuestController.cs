using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using SlateCore.BLL.Interfaces;
using SlateCore.BLL.DTO;
using SlateCORE.Web.Models;
using SlateCORE.Web.Interfaces;
using SlateCORE.Web.Helpers;

namespace SlateCORE.Web.Controllers
{
    [AllowAnonymous]
    public class GuestController : Controller
    {
        private IUserService userService;
        private readonly IFormsAuthenticationService formsAuthentication;

        public GuestController(IUserService service, IFormsAuthenticationService authentification)
        {
            userService = service;
            formsAuthentication = authentification;
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View("Registration");
        }

        [HttpPost]
        public async Task<ActionResult> Registration(UserAccountViewModel account)
        {
            Mapper.Initialize(config => config.CreateMap<UserAccountViewModel, UserAccountDTO>());
            var newAccount = Mapper.Map<UserAccountViewModel, UserAccountDTO>(account);
            await userService.Create(newAccount);
            formsAuthentication.SetAuthCookie(account.Email, true);

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public ActionResult Authorization()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Authorization(UserAccountViewModel account)
        {
            var login = account.Email;
            var foundedAccount = await userService.GetByLogin(login);

            if (foundedAccount != null)
            {
                var passwordHash = AccountService.CreatePasswordHash(foundedAccount.PasswordHash, foundedAccount.Salt);
                if (foundedAccount.PasswordHash.Equals(passwordHash))   //Или как решили сравнивать значения?
                {
                    formsAuthentication.SetAuthCookie(account.Email, true);
                    return RedirectToAction("Index", "Account");
                }
            }

            ModelState.AddModelError("", "Error! Please check the entered data!");

            return View("Login", account);
        }
    }
}