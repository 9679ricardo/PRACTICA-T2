using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalidadT2.Constantes;
using CalidadT2.interfaces;
using CalidadT2.Models;
using CalidadT2.servives;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CalidadT2.Controllers
{
    [Authorize]
    public class BibliotecaController : Controller
    {
        private readonly IBiblioteca mBiblioteca;
        private readonly IUsuario mUsuario;
        public BibliotecaController( IBiblioteca mBiblioteca, IUsuario mUsuario)
        {

            this.mBiblioteca = mBiblioteca;
            this.mUsuario = mUsuario;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var claim = HttpContext.User.Claims;

            Usuario user = mUsuario.LoggedUser(claim);

            var model = mBiblioteca.getList(user.Id);

            return View(model);
        }

        [HttpGet]
        public ActionResult Add(int libro)
        {
            var claim = HttpContext.User.Claims;

            Usuario user = mUsuario.LoggedUser(claim);

            var biblioteca = mBiblioteca.cretateNew(libro, user.Id);

            mBiblioteca.addDB(biblioteca);

            TempData["SuccessMessage"] = "Se añádio el libro a su biblioteca";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MarcarComoLeyendo(int libroId)
        {
            var claim = HttpContext.User.Claims;

            Usuario user = mUsuario.LoggedUser(claim);

            mBiblioteca.marcarComoLeyendo(libroId, user.Id);

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult MarcarComoTerminado(int libroId)
        {
            var claim = HttpContext.User.Claims;

            Usuario user = mUsuario.LoggedUser(claim);

            mBiblioteca.marcarComoTerminado(libroId, user.Id);

            TempData["SuccessMessage"] = "Se marco como leyendo el libro";

            return RedirectToAction("Index");
        }
    }
}
