using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Repositories;

namespace tp03.Controllers
{
    public class CourierController : Controller
    {
        private readonly DataContext _db;
        private readonly ILogger<CourierController> _logger;
        private readonly IMapper mapper;

        public CourierController(ILogger<CourierController> logger, DataContext DB)
        {
            _logger = logger;
            _db = DB;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
