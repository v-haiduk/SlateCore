using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SlateCore.BLL.Interfaces;
using SlateCORE.Web.Interfaces;
using SlateCORE.Web.Models;

namespace SlateCORE.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IUserService userService;
        private readonly IFormsAuthenticationService formsAuthentication;

        public AccountController(IUserService service, IFormsAuthenticationService authentification)
        {
            userService = service;
            formsAuthentication = authentification;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult LogOff(UserAccountViewModel user)
        {
            formsAuthentication.SignOut();

            return View("Index");
        }
    }
}