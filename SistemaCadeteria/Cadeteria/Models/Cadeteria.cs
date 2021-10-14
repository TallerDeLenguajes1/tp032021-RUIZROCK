using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadeteria.Models
{
    public class Cadeteria
    {
        private string nombre;
        public List<Cadete> ListaCadetes;
        public List<Pedidos> ListaPedidos;

        public Cadeteria()
        {
            ListaCadetes = new List<Cadete>();
            ListaPedidos = new List<Pedidos>();
        }
    }
}
