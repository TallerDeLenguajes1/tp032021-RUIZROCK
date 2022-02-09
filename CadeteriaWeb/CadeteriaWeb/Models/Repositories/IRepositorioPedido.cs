using CadeteriaWeb.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public interface IRepositorioPedido
    {
        void DeletePedido(int id);
        List<Pedido> GetAllPedidos(int cadeteID = 0);
        Pedido GetPedidoById(int id);
        void InsertPedido(Pedido item);
        void UpdatePedido(Pedido item);
    }
}
