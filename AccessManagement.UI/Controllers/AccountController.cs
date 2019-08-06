using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AccessManagement.UI.DataLayer;
using AccessManagement.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.UI.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var user = _context.Users.FirstOrDefault(c => c.UserName == loginModel.UserName && c.Password == loginModel.Password);

            if (user != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Name, loginModel.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                ClaimsIdentity claimsIdentity = new
                    ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddDays(10)
                });
            }




            return View(loginModel);
        }

        [Route("accessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}