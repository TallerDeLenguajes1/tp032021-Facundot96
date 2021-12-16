using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.ViewModels;

namespace tp03.Models.Repositories
{
    public interface IRepositoryOrders
    {
        List<Order> GetAll();
        void AddOrder(Order _Order);
        void AddOrderDeliveryM(Order _Order, DeliveryM deliveryM);
        void CancelOrder(int _OrderCode);
        void FinishOrder(int _OrderCode);
        void AcceptOrder(int _OrderCode, int code);
        OrderViewModel GetOneDeliveryMClient(DataContext _DB);
        List<Order> GetAllOrdersClient(int _ClientCode);
        List<Order> GetAllAvailable();
        List<Order> GetAllOrdersDeliveryM(int _ClientCode);
    }
}
