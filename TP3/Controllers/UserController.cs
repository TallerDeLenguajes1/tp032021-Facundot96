using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;
using TP3.Models.Repositories;
using TP3.Models.ViewModels;

namespace TP3.Controllers
{
    public class UserController : Controller
    {
        private readonly DataContext _db;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper mapper;
        public UserController(ILogger<UserController> logger, DataContext DB, IMapper mapper)
        {
            _logger = logger;
            _db = DB;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            try
            {
                List<User> listadoUsuarios = _db.User.GetAll();
                var listUsuariosViewModel = mapper.Map<List<UserViewModel>>(listadoUsuarios);
                return View(listUsuariosViewModel);
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult AltaUsuario()
        {
            return View(new ApprovedUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AltaUsuario(ApprovedUserViewModel _Usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuariodb = mapper.Map<User>(_Usuario);
                    _db.User.AddUser(usuariodb);
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    return RedirectToAction(nameof(AltaUsuario));
                }
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string _Username, string _Contrasena)
        {
            try
            {
                User usuario = _db.User.StartLogin(_Username, _Contrasena);
                if (usuario.UserType != 0)
                {
                    HttpContext.Session.SetString("Usuario", _Username);
                    HttpContext.Session.SetInt32("Clearance", usuario.UserType);
                    HttpContext.Session.SetInt32("UsuarioID", usuario.Id);
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
