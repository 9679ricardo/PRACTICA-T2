using CalidadT2.Controllers;
using CalidadT2.interfaces;
using CalidadT2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;

namespace CalidadT2Test
{
    public class BibliotecaControllerTest
    {
        [Test]
        public void DebeDevolverListaUsuario()
        {
            var iuser = new Mock<IUsuario>();
            var ibibli = new Mock<IBiblioteca>();


            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ricardo")

            }, "Bear Token"));

            var user = new Usuario()
            {
                Id = 1,
                Nombres = "Ricardo",
                Password = "admin",
                Username = "admin"
            };

            iuser.Setup(u => u.LoggedUser(principal.Claims)).Returns(user);
            ibibli.Setup(b => b.getList(user.Id)).Returns(new List<Biblioteca>());

            var controller = new BibliotecaController( ibibli.Object, iuser.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                }
            };

            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsNotInstanceOf<RedirectToRouteResult>(result);
            Assert.IsInstanceOf<List<Biblioteca>>(result.Model);
        }

        [Test]
        public void DebePoderAgregarUnaBiblioteca()
        {
            var iuser = new Mock<IUsuario>();
            var ibibli = new Mock<IBiblioteca>();

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";
            
            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ricardo")

            }, "Bear Token"));

            var user = new Usuario()
            {
                Id = 1,
                Nombres = "Ricardo",
                Password = "admin",
                Username = "admin"
            };

            var biblioteca = new Biblioteca()
            {
               
            };

            iuser.Setup(u => u.LoggedUser(principal.Claims)).Returns(user);
            ibibli.Setup(b => b.cretateNew(1,1)).Returns(biblioteca);
            ibibli.Setup(a => a.addDB(biblioteca));

            var controller = new BibliotecaController(ibibli.Object, iuser.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                },
                TempData = tempData
            };

            var result = controller.Add(1);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.IsNotInstanceOf<ViewResult>(result);
        }



        [Test]
        public void DebePoderMarcarComoLeyendo()
        {
            var iuser = new Mock<IUsuario>();
            var ibibli = new Mock<IBiblioteca>();

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ricardo")

            }, "Bear Token"));

            var user = new Usuario()
            {
                Id = 1,
                Nombres = "Ricardo",
                Password = "admin",
                Username = "admin"
            };

            iuser.Setup(u => u.LoggedUser(principal.Claims)).Returns(user);
            ibibli.Setup(b => b.marcarComoLeyendo(1, 1));

            var controller = new BibliotecaController( ibibli.Object, iuser.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                },
                TempData = tempData
            };

            var result = controller.MarcarComoLeyendo(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.IsNotInstanceOf<ViewResult>(result);

        }

        [Test]
        public void DebePoderMarcarComoTerminado()
        {
            var iuser = new Mock<IUsuario>();
            var ibibli = new Mock<IBiblioteca>();

            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ricardo")

            }, "Bear Token"));

            var user = new Usuario()
            {
                Id = 1,
                Nombres = "Ricardo",
                Password = "admin",
                Username = "admin"
            };

            iuser.Setup(u => u.LoggedUser(principal.Claims)).Returns(user);
            ibibli.Setup(b => b.marcarComoTerminado(1, 1));

            var controller = new BibliotecaController( ibibli.Object, iuser.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        User = principal
                    }
                },
                TempData = tempData
            };

            var result = controller.MarcarComoLeyendo(1);
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            Assert.IsNotInstanceOf<ViewResult>(result);

        }

    }
}
