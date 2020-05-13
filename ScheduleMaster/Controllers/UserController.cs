using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ScheduleMaster.Models;
using ScheduleMaster.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace ScheduleMaster.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _sqlUsersService;
        private readonly ICyberSecurityProvider _cyberSecurity;

        public UserController(IUsersService usersService, ICyberSecurityProvider cyberSecurity)
        {
            _sqlUsersService = usersService;
            _cyberSecurity = cyberSecurity;
        }
        public IActionResult Index()
        {
            return Redirect($"../Home/Index");
        }

        public ActionResult MyAjaxGET()
        {
            string temp = Request.Query["userName"];

            // Perform your operation  

            return Json(temp);
        }
        [HttpPost]
        public ActionResult NewUser()
        {
            var name = Request.Form["username"];
            var email = Request.Form["email"];
            var encryptedPassword = _cyberSecurity.EncryptPassword(Request.Form["pwd"]);

            try
            {
                _sqlUsersService.AddUser(email, name, encryptedPassword);
            }
            catch (Npgsql.PostgresException)
            {

                return Redirect("InvalidUsername");
            }

            return Redirect("Login");
        }

        public IActionResult InvalidUsername()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> LoginAsync([FromForm] string username, [FromForm] string password)
        {
            if (!_cyberSecurity.IsValidUser(username, password))
            {
                return RedirectToAction("Index", "Home");
            }

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, username) };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            { };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("DetailsOfCurrentUser", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}