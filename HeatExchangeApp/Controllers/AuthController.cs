using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;
using HeatExchangeApp.Models.Additional_Classes;

namespace WebTeploobmenApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly DataBaseContext _context;
        public AuthController(DataBaseContext context)
        {

            _context = context;
        }
        public async Task<IActionResult> Logout(string login, string password)
        {

            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string login, string password)
        {
            var user = _context.User.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim> {
                    new("id_user", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,login) };
                // создаем объект ClaimsIdentity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                // установка аутентификационных куки
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
