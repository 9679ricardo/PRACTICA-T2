using CalidadT2.Controllers;
using CalidadT2.interfaces;
using CalidadT2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CalidadT2Test
{
    public class AuthControllerTest
    {
        [Test]
        public void NoDebePoderHacerLogin()
        {
            var iuser = new Mock<IUsuario>();

            var user = new Usuario()
            {
                Id = 1,
                Nombres = "Ricardo",
                Password = "admin",
                Username = "admin"
            };

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);


            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["SessionVariable"] = "admin";

            iuser.Setup(p => p.login(user.Username, user.Password)).Returns(user);


            var principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Ricardo")

            }, "Bear Token"));

            var de = new DefaultHttpContext()
            {
                RequestServices = serviceProviderMock.Object,
                User = principal
            };

            var controller = new AuthController(null, iuser.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = de,
                    
                },

                TempData = tempData
            };
           
            var redirect = controller.Login(user.Username, user.Password);
            Assert.IsInstanceOf<ActionResult>(redirect);
            Assert.IsNotInstanceOf<ViewResult>(redirect);
        }

        //  public ControllerContext MockControllerContext()
        //{
        //    var controllerContext = new Mock<ControllerContext>();
        //    var httpContext = new Mock<HttpContextBase>();
        //    controllerContext.Setup(o => o.HttpContext).Returns(httpContext.Object);
        //    return controllerContext.Object;
        //}
      
    }
}
