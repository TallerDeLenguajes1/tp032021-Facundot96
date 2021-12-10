using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SystemEntities;
using TP3.Models.Entities;
using TP3.Models.Repositories;

namespace TP3.ViewComponents
{
    [ViewComponent(Name = "Usuario")]
    public class UserViewComponent : ViewComponent
    {
        private readonly DataContext _db;

        public UserViewComponent(DataContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }
        private Task<List<MenuOptions>> GetItemsAsync()
        {
            List<MenuOptions> resultado;
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                resultado = _db.User.ObtainOptions(Convert.ToInt32(HttpContext.Session.GetInt32("Clearance")));
            }
            else
            {
                resultado = _db.User.ObtainOptions(0);
            }

            return Task.FromResult(resultado);
        }
    }
}
