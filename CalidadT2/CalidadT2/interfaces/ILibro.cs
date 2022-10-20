using CalidadT2.Models;
using System.Collections.Generic;

namespace CalidadT2.interfaces
{
    public interface ILibro
    {
        List<ILibro> getListLibro(int id);
        ILibro getLibro(int id);
        void addLibro(int id);
        void addComentario(Comentario comentario, int id);
    }
}
