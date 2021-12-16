using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.ViewModels
{
    public class DeliveryMViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        [StringLength(100)]
        public string PhoneNum { get; set; }
        public List<Order> OrderList { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string CourierId { get; set; }
        public int OrdersComplete { get; set; }
        public int OrdersActive { get; set; }
        public int Active { get; set; }
        public List<Courier> CourierList { get; set; }
        public DeliveryM DeliveryM;
        public DeliveryMViewModel() { }
    }
    public class ApproveDeliveryViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100)]
        public string Address { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        [StringLength(100)]
        public string PhoneNum { get; set; }
        public List<Order> OrderList { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string CourierId { get; set; }
        public List<Courier> CourierList { get; set; }
        public ApproveDeliveryViewModel() { }

    }

    public class DeleteDeliveryMViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNum { get; set; }
        public int OrdersComplete { get; set; }
        public int OrdersActive { get; set; }
        public DeleteDeliveryMViewModel() { }
    }
}
