using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioUsuario
    {
        void DeleteUsuario(int id);
        void InsertUsuario(Usuario item);
        void UpdateUsuario(Usuario item);
        Usuario Validate(string username, string password);
        List<Usuario> GetAllUsuarios();
        Usuario GetUsuarioById(int id);
        Usuario GetUsuarioByName(string username);
    }
}
