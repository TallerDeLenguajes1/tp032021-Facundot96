using AutoMapper;
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
    public class DeliveryMController : Controller
    {
        private readonly DataContext _db;
        private readonly ILogger<DeliveryMController> _logger;
        private readonly IMapper mapper;
        public DeliveryMController(ILogger<DeliveryMController> logger, DataContext DB, IMapper mapper)
        {
            _logger = logger;
            _db = DB;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            try
            {
                List<DeliveryM> listaCadetes = _db.DeliveryM.getAll();
                var listCadetes = mapper.Map<List<DeliveryMViewModel>>(listaCadetes);
                return View(listCadetes);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }

        public IActionResult IndexReadmit()
        {
            try
            {
                List<DeliveryM> listaCadetes = _db.DeliveryM.getAll();
                var listCadetes = mapper.Map<List<DeliveryMViewModel>>(listaCadetes);
                return View(listCadetes);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }

        public IActionResult AltaCadete()
        {
            try
            {
                List<Courier> listaCadeterias = _db.Courier.getAll();
                ApproveDeliveryMViewModel listCadeterias = new ApproveDeliveryMViewModel()
                {
                    CourierList = listaCadeterias
                };
                return View(listCadeterias);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }
        public ActionResult AgregarCadete()
        {
            return View(new ApproveDeliveryMViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AgregarCadete(ApproveDeliveryMViewModel _DeliveryM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cadetedb = mapper.Map<DeliveryM>(_DeliveryM);
                    _db.DeliveryM.AddDeliveryM(cadetedb);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(AgregarCadete));
                }
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult DeleteCadete(int _Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DeliveryM cadete = _db.DeliveryM.GetOne(_Id, 0);
                    var cadeteVM = mapper.Map<DeleteDeliveryMViewModel>(cadete);
                    return View(cadeteVM);
                }
                else
                {
                    return RedirectToAction(nameof(Modify));
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }

        public IActionResult DeleteForGoodCadete(int _Id)
        {
            _db.DeliveryM.DeleteDeliveryM(_Id);
            return RedirectToAction("Index");
        }

        public IActionResult ReadmitCadete(int _Id)
        {
            _db.DeliveryM.ReadmitDeliveryM(_Id);
            return RedirectToAction("Index");
        }

        public IActionResult Modify(int _Id)
        {
            try
            {
                var cadeteModificar = _db.DeliveryM.GetOneCourier(_Id, _db);
                return View(cadeteModificar);
            }
            catch
            {
                return NotFound();
            }
        }

        public IActionResult ModifyForGoodCadete(DeliveryMViewModel _DeliveryM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var cadetedb = mapper.Map<DeliveryM>(_DeliveryM);
                    _db.DeliveryM.ModifyDeliveryM(cadetedb);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Modify));
                }
            }
            catch
            {
                return NotFound();
            }
        }
    }
}

