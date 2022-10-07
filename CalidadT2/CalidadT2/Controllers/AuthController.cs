using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CalidadT2.interfaces;
using CalidadT2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CalidadT2.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppBibliotecaContext app;
        private readonly IUsuario mUsuario;
        public AuthController(AppBibliotecaContext app, IUsuario mUsuario)
        {
            this.app = app;
            this.mUsuario = mUsuario;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var usuario = mUsuario.login(username, password);
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                HttpContext.SignInAsync(claimsPrincipal);
                
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
