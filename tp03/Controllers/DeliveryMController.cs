using AutoMapper;
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
                List<DeliveryM> deliveryMList = _db.DeliveryMs.GetAll();
                var listDeliveryMViewModel = mapper.Map<List<DeliveryMViewModel>>(deliveryMList);
                return View(listDeliveryMViewModel);
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
                List<DeliveryM> deliveryMList = _db.DeliveryMs.GetAll();
                var listDeliveryMViewModel = mapper.Map<List<DeliveryMViewModel>>(deliveryMList);
                return View(listDeliveryMViewModel);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }

        public IActionResult ApproveDeliveryM()
        {
            try
            {
                List<Courier> courierList = _db.Couriers.GetAll();
                ApproveDeliveryViewModel listDeliveryMViewModel = new ApproveDeliveryViewModel()
                {
                    CourierList = courierList
                };
                return View(listDeliveryMViewModel);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }
        public ActionResult AddDeliveryM()
        {
            return View(new ApproveDeliveryViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDeliveryM(ApproveDeliveryViewModel _DeliveryM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deliverymdb = mapper.Map<DeliveryM>(_DeliveryM);
                    _db.DeliveryMs.AddDeliveryM(deliverymdb);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(AddDeliveryM));
                }
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult DeleteDeliveryM(int _Id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DeliveryM deliveryM = _db.DeliveryMs.GetOne(_Id, 0);
                    var deliveryMVM = mapper.Map<DeleteDeliveryMViewModel>(deliveryM);
                    return View(deliveryMVM);
                }
                else
                {
                    return RedirectToAction(nameof(ModifyDeliveryM));
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                return NotFound();
            }
        }

        public IActionResult DeleteForGoodDeliveryM(int _Id)
        {
            _db.DeliveryMs.DeleteDeliveryM(_Id);
            return RedirectToAction("Index");
        }

        public IActionResult ReadmitDeliveryM(int _Id)
        {
            _db.DeliveryMs.ReadmitDeliveryM(_Id);
            return RedirectToAction("Index");
        }
        public IActionResult ModifyDeliveryM(int _Id)
        {
            try
            {
                var deliveryMModify = _db.DeliveryMs.GetOneCourier(_Id, _db);
                return View(deliveryMModify);
            }
            catch
            {
                return NotFound();
            }
        }
        public IActionResult ModifyForGoodDeliveryM(DeliveryMViewModel _DeliveryM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deliverymdb = mapper.Map<DeliveryM>(_DeliveryM);
                    _db.DeliveryMs.ModifyDeliveryM(deliverymdb);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(ModifyDeliveryM));
                }
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
