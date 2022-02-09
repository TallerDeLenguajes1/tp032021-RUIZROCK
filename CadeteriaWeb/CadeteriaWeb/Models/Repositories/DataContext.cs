using CadeteriaWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class DataContext
    {
        public IRepositorioCadete Cadetes { get; set; }
        public IRepositorioCliente Clientes { get; set; }
        public IRepositorioPedido Pedidos { get; set; }
        public IRepositorioUsuario Usuarios { get; set; }

        public DataContext(IRepositorioCadete cadetes, IRepositorioCliente clientes, IRepositorioPedido pedidos, IRepositorioUsuario usuarios)
        {
            Cadetes = cadetes;
            Clientes = clientes;
            Pedidos = pedidos;
            Usuarios = usuarios;
        }
    }
}
