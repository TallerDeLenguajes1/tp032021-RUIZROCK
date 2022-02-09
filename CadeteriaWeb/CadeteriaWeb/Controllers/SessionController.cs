using CadeteriaWeb.Entities;
using CadeteriaWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class SessionController : Controller
    {
        internal void SetSesion(Usuario Usuario)
        {
            if (!IsSesionIniciada())
            {
                if (Usuario != null)
                {
                    HttpContext.Session.SetString("Username", Usuario.Username);
                    HttpContext.Session.SetString("Rol", Usuario.Rol.ToString());
                    HttpContext.Session.SetInt32("UserID", Usuario.Id);
                }
            }
        }

        internal bool IsSesionIniciada()
        {
            return (HttpContext.Session.GetString("Username") != null);
        }

        internal string GetRol()
        {
            return IsSesionIniciada()
                ? HttpContext.Session.GetString("Rol")
                : "NOTLOGUED";

        }

        internal string GetUser()
        {
            return IsSesionIniciada()
                ? HttpContext.Session.GetString("Username")
                : null;
        }

        internal int GetIdUsuario()
        {
            return IsSesionIniciada()
                ? (int)HttpContext.Session.GetInt32("UserID")
                : 0;
        }

        internal void Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Rol");
            HttpContext.Session.Remove("UserID");
            HttpContext.Session.Clear();
        }
    }
}
