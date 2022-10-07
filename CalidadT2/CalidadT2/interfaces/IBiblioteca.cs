using CalidadT2.Models;
using System.Collections.Generic;

namespace CalidadT2.interfaces
{
    public interface IBiblioteca
    {
        List<Biblioteca> getList(int id);
        Biblioteca cretateNew(int libro, int userId);
        void addDB(Biblioteca biblioteca);
        void marcarComoLeyendo(int libroId, int userId);
        void marcarComoTerminado(int libroId, int userId);

    }
}
