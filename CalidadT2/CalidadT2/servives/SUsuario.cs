using CalidadT2.interfaces;
using CalidadT2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CalidadT2.servives
{
    public class SUsuario : IUsuario
    {
        private readonly AppBibliotecaContext app;
        public SUsuario(AppBibliotecaContext app)
        {
            this.app = app;
        }

        public Usuario LoggedUser(Claim clain)
        {
            var claim = clain;
            var user = app.Usuarios.Where(o => o.Username == claim.Value).FirstOrDefault();
            return user;
        }

        public Usuario LoggedUser(IEnumerable<Claim> identity)
        {
            string username = identity.FirstOrDefault(o => o.Type == ClaimTypes.Name).Value;
            var user = app.Usuarios.Where(o => o.Username == username).FirstOrDefault();
            return user;
        }

        public Usuario login(string username, string password)
        {
            var usuario = app.Usuarios.Where(o => o.Username == username && o.Password == password).FirstOrDefault();
            return usuario;
        }
    }
}
