using AutoMapper;
using CadeteriaWeb.Entities;
using CadeteriaWeb.Models;
using CadeteriaWeb.Models.ViewModels;
using CadeteriaWeb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Controllers
{
    public class UsuariosController : SessionController
    {
        private readonly DataContext DB;
        private readonly ILogger<UsuariosController> _logger;
        private readonly IMapper mapper;

        public UsuariosController(DataContext DB, ILogger<UsuariosController> logger, IMapper mapper)
        {
            this.DB = DB;
            this._logger = logger;
            this.mapper = mapper;
        }


        // -----------------------------------------------------------------
        // --------------------------INFO USUARIOS--------------------------
        // -----------------------------------------------------------------
        // INDEX: Usuarios/
        public IActionResult InfoUsuario()
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");

            return View(DB.Usuarios.GetUsuarioById(GetIdUsuario()));
        }


        // -----------------------------------------------------------------
        // -------------------------LISTA USUARIOS--------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/IndexAdmin
        public IActionResult ListaUsuarios()
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");

            // solo los admins pueden ver todos los usuarios
            if (GetRol() == "ADMIN")
                return View(DB);

            return RedirectToAction("InfoUsuario");
        }


        // -----------------------------------------------------------------
        // -------------------------LOGUEO USUARIOS-------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/Login
        public IActionResult Login(UsuarioLoginViewModel usuarioVM = null)
        {
            // solo pueden entrar al login quienes no estan en sesion
            if (!IsSesionIniciada())
                return View(usuarioVM);

            return RedirectToAction("InfoUsuario");
        }

        // POST: Usuarios/LoginPost
        [HttpPost]
        public IActionResult LoginPost(UsuarioLoginViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario U = DB.Usuarios.Validate(usuario.Username, usuario.Password); // valida que coincidan

                    if (U != null) // si encuentra una coincidencia
                    {
                        SetSesion(U); // iniciar sesion
                        _logger.LogInformation($"LOGUEO DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");

                        return RedirectToAction("Index", "Home");
                    }
                    else // si no encuentra usuarios con ese user-contraseña
                    {
                        usuario.Message = "Contraseña incorrecta";

                        if (DB.Usuarios.GetUsuarioByName(usuario.Username) == null)
                            usuario.Message = "No existe un usuario con ese nombre";

                        return RedirectToAction("Login", usuario);
                    }
                }

                return RedirectToAction("Login", usuario); // error en el ModelState
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN LOGUEO DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("Login");
            }
        }

        // GET: Usuarios/Logout
        public IActionResult CerrarSesion()
        {
            // solo pueden entrar al login quienes no estan en sesion
            if (IsSesionIniciada())
                Logout();

            return RedirectToAction("Login");
        }


        // -----------------------------------------------------------------
        // ------------------------REGISTRO USUARIOS------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/Register
        public IActionResult Registrate(UsuarioCreateViewModel usuarioVM = null)
        {
            // solo pueden entrar al registro quienes no estan en sesion
            if (!IsSesionIniciada())
                return View(usuarioVM);

            return RedirectToAction("InfoUsuario");
        }

        // POST: Usuarios/RegisterPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterPost(UsuarioCreateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // si ya existe un usuario con ese nombre no se puede crear
                    if (DB.Usuarios.GetUsuarioByName(usuario.Username) != null)
                    {
                        usuario.Message = "Ya existe un usuario con ese nombre";
                        return RedirectToAction("Registrate", usuario);
                    }

                    Usuario U = mapper.Map<Usuario>(usuario);

                    _logger.LogInformation($"REGISTRO DE USUARIO - USERNAME:{U.Username}");

                    DB.Usuarios.InsertUsuario(U);
                    return RedirectToAction("Login");
                }

                return RedirectToAction("Registrate");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN REGISTRO DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("InfoUsuario");
            }
        }


        // -----------------------------------------------------------------
        // -------------------------CARGA USUARIOS--------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/CreateUsuario
        public IActionResult CrearUsuario(UsuarioCreateViewModel usuarioVM = null)
        {
            if (GetRol() != "ADMIN")
                return RedirectToAction("Registrate");

            return View(usuarioVM);
        }

        // POST: Usuarios/CreateUsuarioPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUsuarioPost(UsuarioCreateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // si ya existe un usuario con ese nombre no se puede crear
                    if (DB.Usuarios.GetUsuarioByName(usuario.Username) != null)
                    {
                        usuario.Message = "Ya existe un usuario con ese nombre";
                        return RedirectToAction("CrearUsuario", usuario);
                    }

                    Usuario U = mapper.Map<Usuario>(usuario);

                    _logger.LogInformation($"CREACION DE USUARIO - USERNAME:{U.Username}");

                    DB.Usuarios.InsertUsuario(U);
                    return RedirectToAction("Login");
                }

                return RedirectToAction("CrearUsuario");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN CREACION DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("ListaUsuarios");
            }
        }


        // -----------------------------------------------------------------
        // -------------------------DELETE USUARIOS-------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/DeleteUsuario/{id}
        public IActionResult DeleteUsuario(int id = 0)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");
            if (id == 0)
                return RedirectToAction("InfoUsuario");

            try
            {
                // solo pueden eliminar usuarios el admin y un usuario a si mismo
                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.Usuarios.GetUsuarioById(id);

                    if (U != null && U.Rol != Rol.ADMIN)
                    {
                        DB.Usuarios.DeleteUsuario(id);
                        _logger.LogInformation($"DELETE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");

                        // si se elimina el usuario que esta logueado, se cierra sesion
                        if (GetIdUsuario() == id) Logout();
                    }

                }

                return RedirectToAction("ListaUsuarios");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN DELETE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("ListaUsuarios");
            }
        }


        // -----------------------------------------------------------------
        // -------------------------UPDATE USUARIOS-------------------------
        // -----------------------------------------------------------------
        // GET: Usuarios/UpdateUsuario/{id}
        public IActionResult ModificarUsuario(int id = 0, UsuarioUpdateViewModel usuarioVM = null)
        {
            if (!IsSesionIniciada())
                return RedirectToAction("Login");

            try
            {
                if (usuarioVM.Username != null)
                {
                    if (GetRol() == "ADMIN" || GetIdUsuario() == usuarioVM.Id)
                        return View(usuarioVM);
                }

                // solo pueden modificar usuarios los admin y el usuario a si mismo
                if (GetRol() == "ADMIN" || GetIdUsuario() == id)
                {
                    Usuario U = DB.Usuarios.GetUsuarioById(id);
                    if (U != null && U.Rol != Rol.ADMIN)
                        return View(mapper.Map<UsuarioUpdateViewModel>(U));
                }

                return RedirectToAction("infoUsuario");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("ListaUsuarios");
            }
        }

        // POST: Usuario/UpdateUsuarioPost
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUsuarioPost(UsuarioUpdateViewModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario U = mapper.Map<Usuario>(usuario);

                    // si se cambio el nombre debo revisar que el nombre no este utilizado
                    // reviso si existe en la base un usuario con ese nombre
                    // si existe alguien con ese nombre reviso si tiene el mismo id -> mantiene se nombre anterior
                    // en caso de exitir alguien con ese nombre y distinto id -> el nombre no se puede usar
                    if ((DB.Usuarios.GetUsuarioByName(U.Username) != null) && (DB.Usuarios.GetUsuarioByName(U.Username)?.Id != U.Id))
                    {
                        UsuarioUpdateViewModel usuarioVM = usuario;
                        usuarioVM.Message = "Ya existe un usuario con ese nombre";
                        return RedirectToAction("ModificarUsuario", usuarioVM);
                    }

                    DB.Usuarios.UpdateUsuario(U);
                    _logger.LogInformation($"UPDATE DE USUARIO - ID:{U.Id}, USERNAME:{U.Username}, ROL:{U.Rol}");
                    return RedirectToAction("InfoUsuario");
                }

                return RedirectToAction("ModificarUsuario", usuario); // si hay un error en el ModelState
            }
            catch (Exception e)
            {
                _logger.LogError($"ERROR EN UPDATE DE USUARIO - EXCEPTION:{e.Message}");
                return RedirectToAction("ListaUsuarios");
            }
        }
    }
}
