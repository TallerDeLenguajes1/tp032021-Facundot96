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
                List<Order> listadoPedidos = _db.Order.getAll();
                var listPedidoViewModel = mapper.Map<List<OrderViewModel>>(listadoPedidos);
                return View(listPedidoViewModel);
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult AltaPedido()
        {
            return View(_db.Order.GetOneDeliverUser(_db));
        }
        public IActionResult AgregarPedido()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarPedido(string Observacion, string Direccion)
        {
            try
            {
                Order pedido = new Order()
                {
                    ClientId = Convert.ToInt32(HttpContext.Session.GetString("UsuarioID")),
                    Adress = Direccion,
                    Observation = Observacion

                };
                _db.Order.AddOrder(pedido);
                if (HttpContext.Session.GetInt32("Clearance") != 1)
                {
                    return RedirectToAction(nameof(EstadoPedidos));
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
        //No recuerdo usarlo pero lo dejo por las dudas rompa algo
        public IActionResult ListaPedidos()
        {
            return View(_db.Order.getAll());
        }
        public IActionResult EstadoPedidos()
        {
            try
            {
                List<Order> listadoPedidos = _db.Order.GetAllOrdersClient(Convert.ToInt32(HttpContext.Session.GetString("UsuarioID")));
                var listPedidoViewModel = mapper.Map<List<OrderViewModel>>(listadoPedidos);
                return View(listPedidoViewModel);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult CancelarPedido(int _Id)
        {
            _db.Order.CancelOrder(_Id);
            return RedirectToAction("EstadoPedidos");
        }
    }
}
