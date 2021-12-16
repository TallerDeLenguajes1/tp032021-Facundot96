using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.Repositories;
using tp03.Models.ViewModels;

namespace tp03.Controllers
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
                List<User> userList = _db.Users.GetAll();
                var listUserViewModel = mapper.Map<List<UserViewModel>>(userList);
                return View(listUserViewModel);
            }
            catch
            {
                return NotFound();
            }
        }

        public ActionResult ApproveUser()
        {
            return View(new ApproveUserViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApproveUser(ApproveUserViewModel _User)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userdb = mapper.Map<User>(_User);
                    _db.Users.AddUser(userdb);
                    return RedirectToAction(nameof(Index), "Home");
                }
                else
                {
                    return RedirectToAction(nameof(ApproveUser));
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
        public IActionResult Login(string _Username, string _Password)
        {
            try
            {
                User user = _db.Users.StartLogin(_Username, _Password);
                if (user.Type != 0)
                {
                    HttpContext.Session.SetString("Usuario", _Username);
                    HttpContext.Session.SetInt32("Clearance", user.Type);
                    HttpContext.Session.SetString("UsuarioID", user.UserId);
                    HttpContext.Session.SetString("Codigo", user.Code);
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
