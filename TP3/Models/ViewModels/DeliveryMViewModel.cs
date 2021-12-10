using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TP3.Models.Entities;

namespace TP3.Models.ViewModels
{
    public class DeliveryMViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100)]
        public string Adress { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        [StringLength(100)]
        public string PhoneNum { get; set; }
        public List<Order> OrderList { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string Courier { get; set; }
        public int  OrdersComplete { get; set; }
        public int OrdersActive { get; set; }
        public int Active { get; set; }
        public List<Courier> CourierList { get; set; }
        public DeliveryM DeliveryM;
        public DeliveryMViewModel() { }
    }

    public class ApproveDeliveryMViewModel
    {
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo dirección es requerido")]
        [StringLength(100)]
        public string Adress { get; set; }
        [Required(ErrorMessage = "El campo telefono es requerido")]
        [StringLength(100)]
        public string PhoneNum { get; set; }
        public List<Order> OrderList { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [StringLength(100)]
        public string Courier { get; set; }
        public List<Courier> CourierList { get; set; }
        public ApproveDeliveryMViewModel() { }
    }

    public class DeleteDeliveryMViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string PhoneNum { get; set; }
        public int OrdersComplete { get; set; }
        public int OrdersActive { get; set; }
        public DeleteDeliveryMViewModel() { }
    }

}
