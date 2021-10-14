using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly DBTemporal _DB;
        public PedidoController(ILogger<PedidoController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MostrarPedidos()
        {
            return View(_DB.Cadeteria);
        }

        public IActionResult crearPedido(string obs, string estado, string nombreC, string idC, string direcC, string telC)
        {
            int ultimoNum = 100;
            if (_DB.Cadeteria.ListaPedidos.Count() > 0)
            {
                ultimoNum = _DB.Cadeteria.ListaPedidos.Max(x => x.NumeroPedido);
            }
            ultimoNum++;
            Pedidos nuevoPedido = new Pedidos(ultimoNum, obs, idC, nombreC, direcC, telC, estado);
            _DB.Cadeteria.ListaPedidos.Add(nuevoPedido);
            _DB.GuardarPedido(_DB.Cadeteria.ListaPedidos);
            return View("MostrarPedidos", _DB.Cadeteria);
        }

        public IActionResult AsignarCadete(int idPedido, int idCadete)
        {
            QuitarPedidoDeCadete(idPedido);

            if (idCadete != 0)
            {
                Cadete cadete = _DB.Cadeteria.ListaCadetes.Where(a => a.Id == idCadete).First();
                Pedidos pedido = _DB.Cadeteria.ListaPedidos.Where(b => b.NumeroPedido == idPedido).First();
                cadete.ListadoPedidos.Add(pedido);
            }

            return View("MostrarPedidos", _DB.Cadeteria);
        }

        private void QuitarPedidoDeCadete(int idPedido)
        {
            Pedidos pedido = _DB.Cadeteria.ListaPedidos.Find(x => x.NumeroPedido == idPedido);
            _DB.Cadeteria.ListaCadetes.ForEach(cadete => cadete.ListadoPedidos.Remove(pedido));

        }

        public IActionResult eliminarPedido(int numPedido)//PROBLEMA: llega en 0
        {
            for (int i = 0; i < _DB.Cadeteria.ListaPedidos.Count(); i++)
            {
                if (_DB.Cadeteria.ListaPedidos[i].NumeroPedido == numPedido)
                {
                    _DB.Cadeteria.ListaPedidos.Remove(_DB.Cadeteria.ListaPedidos[i]);
                    //_DB.BorrarPedido(numPedido);
                    break;
                }
            }

            return View("MostrarPedidos");
        }

        public IActionResult modificarPedido(int numPedido)//PROBLEMA: llega en 0
        {
            Pedidos pedidoAModificar = null;
            for (int i = 0; i < _DB.Cadeteria.ListaPedidos.Count(); i++)
            {
                if (_DB.Cadeteria.ListaPedidos[i].NumeroPedido == numPedido)
                {
                    pedidoAModificar = _DB.Cadeteria.ListaPedidos[i];
                    break;
                }
            }

            if (pedidoAModificar != null)
            {
                return View("ModificarPedido", pedidoAModificar);
            }
            else
            {
                return View("MostrarPedidos");
            }
        }

        public IActionResult cambiarDatosPedido(int numPedido, string obs, string estado, string nombreC, string idC, string direcC, string telC)
        {
            if (numPedido > 0)
            {
                Pedidos pedidoAModificar = new Pedidos(numPedido, obs, idC, nombreC, direcC, telC, estado);
                //_DB.ModificarPedido(pedidoAModificar);
                return View("MostrarPedidos");
            }
            else
            {
                return View("MostrarPedidos");
            }
        }
    }
}
