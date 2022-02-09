using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioCliente
    {
        void DeleteCliente(int id);
        List<Cliente> GetAllClientes();
        Cliente GetClienteById(int id);
        Cliente GetClienteByInfo(string nombre, string direccion, string telefono);
        void InsertCliente(Cliente item);
        void UpdateCliente(Cliente item);
    }
}
