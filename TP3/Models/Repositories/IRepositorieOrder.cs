using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;
using TP3.Models.ViewModels;

namespace TP3.Models.Repositories
{
    public interface IRepositorieOrder
    {
        List<Order> getAll();
        void AddOrder(Order _Order);
        void AddOrderDeliver(Order _Order, DeliveryM _DeliveryM);
        void CancelOrder(Order _Order);
        void FinishOrder(Order _Order);
        OrderViewModel GetOneDeliverUser(DataContext _DB);
        List<Order> GetAllOrdersClient(int _ClientId);
        void CancelOrder(int _Id);



    }
}
