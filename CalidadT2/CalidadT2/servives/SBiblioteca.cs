using CalidadT2.Constantes;
using CalidadT2.interfaces;
using CalidadT2.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CalidadT2.servives
{
    public class SBiblioteca : IBiblioteca
    {
        private readonly AppBibliotecaContext app;
        public SBiblioteca(AppBibliotecaContext app)
        {
            this.app = app;
        }

        public void addDB(Biblioteca biblioteca)
        {
            app.Bibliotecas.Add(biblioteca);
            app.SaveChanges();
        }

        public Biblioteca cretateNew(int libro, int userId)
        {
            var biblioteca = new Biblioteca
            {
                LibroId = libro,
                UsuarioId = userId,
                Estado = ESTADO.POR_LEER
            };

            return biblioteca;
        }

        public List<Biblioteca> getList(int id)
        {

            var model = app.Bibliotecas
                .Include(o => o.Libro.Autor)
                .Include(o => o.Usuario)
                .Where(o => o.UsuarioId == id)
                .ToList();

            return model;
        }

        public void marcarComoLeyendo(int libroId, int userId)
        {
            var libro = app.Bibliotecas
                .Where(o => o.LibroId == libroId && o.UsuarioId == userId)
                .FirstOrDefault();

            libro.Estado = ESTADO.LEYENDO;
            app.SaveChanges();
        }

        public void marcarComoTerminado(int libroId, int userId)
        {
            var libro = app.Bibliotecas
              .Where(o => o.LibroId == libroId && o.UsuarioId == userId)
              .FirstOrDefault();

            libro.Estado = ESTADO.TERMINADO;
            app.SaveChanges();
        }
    }
}
