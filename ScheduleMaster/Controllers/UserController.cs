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
        private readonly ISQLlogger _sqlLogger;

        public UserController(IUsersService usersService, ICyberSecurityProvider cyberSecurity, ISQLlogger sqlLogger)
        {
            _sqlUsersService = usersService;
            _cyberSecurity = cyberSecurity;
            _sqlLogger = sqlLogger;
        }


        [HttpPost]
        public ActionResult NewUser()
        {
            var name = Request.Form["username"];
            var email = Request.Form["email"];
            var encryptedPassword = _cyberSecurity.EncryptPassword(Request.Form["pwd"]);

            try
            {
                _sqlUsersService.AddUser(name, email, encryptedPassword);
                var userId = _sqlUsersService.GetUserId(email);

                _sqlLogger.Log(userId, "User registered");
            }
            catch (Npgsql.PostgresException)
            {
                return Json("Email address already taken!");
            }

            return Json("Success!");
        }



        [HttpPost]
        public async Task<ActionResult> LoginAsync()
        {
            var password = Request.Form["pwd"];
            var email = Request.Form["email"];
            if (!_cyberSecurity.IsValidUser(email, password))
            {
                return Json(0);
            }

            var userId = _sqlUsersService.GetUserId(email);

            await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("Id", userId.ToString()),
                        new Claim("Email", email),
                    }, CookieAuthenticationDefaults.AuthenticationScheme)),
                    new AuthenticationProperties());

            _sqlLogger.Log(userId, "Logged in");
            return Json(_sqlUsersService.GetUserId(email));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            int userId = int.Parse(HttpContext.User.FindFirstValue("Id"));
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _sqlLogger.Log(userId, "Logged out");
            return Json("Logged out");
        }
    }
}