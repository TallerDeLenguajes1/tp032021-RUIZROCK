using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cadeteria.Models;

namespace Cadeteria.Controllers
{
    public class CadeteController : Controller
    {
        private readonly ILogger<CadeteController> _logger;
        private readonly DBTemporal _DB;

        public CadeteController(ILogger<CadeteController> logger, DBTemporal DB)
        {
            _logger = logger;
            _DB = DB;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MostrarCadetes()
        {
            return View(_DB.Cadeteria.ListaCadetes);
        }

        public IActionResult crearCadete(string nombre, string direc, string tel)
        {
            int ultimoId = 0;
            if (_DB.Cadeteria.ListaCadetes.Count() > 0)
            {
                ultimoId = _DB.Cadeteria.ListaCadetes.Max(x => x.Id);
            }
            ultimoId++;
            Cadete nuevoCadete = new Cadete(ultimoId, nombre, direc, tel);
            _DB.Cadeteria.ListaCadetes.Add(nuevoCadete);
            _DB.GuardarCadete(_DB.Cadeteria.ListaCadetes);
            return View("MostrarCadetes", _DB.Cadeteria.ListaCadetes);
        }

        public IActionResult eliminarCadete(int id) 
        {
            for(int i=0; i < _DB.Cadeteria.ListaCadetes.Count(); i++)
            {
                if(_DB.Cadeteria.ListaCadetes[i].Id == id)
                {
                    _DB.Cadeteria.ListaCadetes.Remove(_DB.Cadeteria.ListaCadetes[i]);
                    _DB.BorrarCadete(id);
                    break;
                }
            }

            return View("MostrarCadetes", _DB.Cadeteria.ListaCadetes);
        }

        public IActionResult modificarCadete(int id)
        {
            Cadete cadeteAModificar = null;
            for (int i = 0; i < _DB.Cadeteria.ListaCadetes.Count(); i++)
            {
                if (_DB.Cadeteria.ListaCadetes[i].Id == id)
                {
                    cadeteAModificar = _DB.Cadeteria.ListaCadetes[i];
                    break;
                }
            }

            if(cadeteAModificar != null)
            {
                return View("ModificarCadete", cadeteAModificar);
            }
            else
            {
                return Redirect("MostrarCadetes");
            }
        }

        public IActionResult cambiarDatosCadete(int id, string nombre, string direc, string tel)
        {
            if (id > 0)
            {
                
                Cadete cadeteAModificar = new Cadete(id, nombre, direc, tel);
                _DB.ModificarCadete(cadeteAModificar);
                return Redirect("MostrarCadetes");
            }
            else
            {
                return View("MostrarCadetes");
            }
        }

    }

    
}
