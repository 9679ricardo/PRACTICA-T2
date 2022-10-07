using CalidadT2.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CalidadT2.interfaces
{
    public interface IUsuario
    {
        Usuario login(string username, string password);
        Usuario LoggedUser(IEnumerable<Claim> identity);
    }
}
