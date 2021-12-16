using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.Repositories;

namespace tp03.ViewComponents
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
            List<MenuOptions> result;
            if (HttpContext.Session.GetString("Usuario") != null)
            {
                result = _db.Users.GetOptions(Convert.ToInt32(HttpContext.Session.GetInt32("Clearance")));
            }
            else
            {
                result = _db.Users.GetOptions(0);
            }

            return Task.FromResult(result);
        }
    }
}
