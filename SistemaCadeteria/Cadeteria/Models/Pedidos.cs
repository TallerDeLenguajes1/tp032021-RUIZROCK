using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Pedidos
    {
        private int numeroPedido;
        private string observaciones, estado;
        private Cliente cliente;

        public Pedidos(int NumeroPedido, string Observaciones, string idCliente, string nombreCliente, string direccionCliente, string telefonoCliente, string Estado)
        {
            this.numeroPedido = NumeroPedido;
            this.observaciones = Observaciones;
            this.estado = Estado;
            this.cliente = new Cliente(idCliente, nombreCliente, direccionCliente, telefonoCliente);
        }

        public int NumeroPedido { get => numeroPedido; set => numeroPedido = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public string Estado { get => estado; set => estado = value; }
    }
}
