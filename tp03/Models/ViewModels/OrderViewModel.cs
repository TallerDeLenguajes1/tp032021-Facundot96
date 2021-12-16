using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.ViewModels
{
    public class OrderViewModel
    {
        public int Number;
        [Required(ErrorMessage = "El campo Observacion es requerido")]
        [StringLength(100)]
        public string Observation;
        public int ClientId;
        [Required(ErrorMessage = "El campo Estado es requerido")]
        [StringLength(100)]
        public string Status;
        [Required(ErrorMessage = "El campo Dirección es requerido")]
        [StringLength(100)]
        public string Address;
        public int DeliverCode;
        public List<DeliveryM> DeliveryM { get; set; }
        public OrderViewModel() { }
        public Order Order { get; set; }
    }

    public class ApproveOrderViewModel
    {
        public int ClientId;
        [Required(ErrorMessage = "El campo observación es requerido")]
        [StringLength(100)]
        public string Observation { get; set; }
        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100)]
        public string Address { get; set; }
        public ApproveOrderViewModel() { }
    }
    public class ModifyOrderViewModel
    {
        public int Number;
        [Required(ErrorMessage = "El campo Observacion es requerido")]
        [StringLength(100)]
        public string Observation;
        public int ClientId;
        [Required(ErrorMessage = "El campo Estado es requerido")]
        [StringLength(100)]
        public string Status;
        public int DeliverCode;
        public ModifyOrderViewModel() { }
    }
}
