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
    public class OrderController : Controller
    {
        private readonly DataContext _db;
        private readonly ILogger<OrderController> _logger;
        private readonly IMapper mapper;

        public OrderController(ILogger<OrderController> logger, DataContext DB, IMapper mapper)
        {
            _logger = logger;
            _db = DB;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            try
            {
                List<Order> orderList = _db.Orders.GetAll();
                var listOrderViewModel = mapper.Map<List<OrderViewModel>>(orderList);
                return View(listOrderViewModel);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult ApproveOrder()
        {
            return View(_db.Orders.GetOneDeliveryMClient(_db));
        }
        public IActionResult AddOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrder(string Observation,string Address)
        {
            try
            {
                Order order = new Order()
                {
                    ClientId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioID")),
                    Address = Address,
                    Observation = Observation
                };
                _db.Orders.AddOrder(order);
                if (HttpContext.Session.GetInt32("Clearance") != 1)
                {
                    return RedirectToAction(nameof(DeliverStatus));
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult OrderList()
        {
            try
            {
                List<Order> orderList = _db.Orders.GetAllAvailable();
                var listOrderViewModel = mapper.Map<List<OrderViewModel>>(orderList);
                return View(listOrderViewModel);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult CancelOrder(int _Id)
        {
            _db.Orders.CancelOrder(_Id);
            if (HttpContext.Session.GetInt32("Clearance") == 3)
            {
                return RedirectToAction("ListaPedidosCadete");
            }
            else
            {
                return RedirectToAction("EstadoPedidos");
            }
        }
        public IActionResult AcceptOrder(int _Id)
        {
            _db.Orders.AcceptOrder(_Id, Convert.ToInt32(HttpContext.Session.GetString("Codigo")));
            return RedirectToAction("OrderList");
        }
        public IActionResult ListOrdersDeliveryM(int _Id)
        {
            try
            {
                List<Order> orderList = _db.Orders.GetAllOrdersDeliveryM(Convert.ToInt32(HttpContext.Session.GetString("Codigo")));
                var listOrderViewModel = mapper.Map<List<OrderViewModel>>(orderList);
                return View(listOrderViewModel);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult DeliverStatus()
        {
            try
            {
                List<Order> orderList = _db.Orders.GetAllOrdersClient(Convert.ToInt32(HttpContext.Session.GetString("UsuarioID")));
                var listOrderViewModel = mapper.Map<List<OrderViewModel>>(orderList);
                return View(listOrderViewModel);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
