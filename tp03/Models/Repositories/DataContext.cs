using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Repositories
{
    public class DataContext
    {
        public IRepositoryDeliveryMs DeliveryMs { get; set; }
        public IRepositoryOrders Orders { get; set; }
        public IRepositoryUsers Users { get; set; }
        public IRepositoryClients Clients { get; set; }
        public IRepositoryCouriers Couriers { get; set; }

        public DataContext(IRepositoryDeliveryMs DeliveryMs, IRepositoryOrders Orders, IRepositoryUsers Users, IRepositoryClients Clients, IRepositoryCouriers Couriers)
        {
            this.DeliveryMs = DeliveryMs;
            this.Orders = Orders;
            this.Users = Users;
            this.Clients = Clients;
            this.Couriers = Couriers;
        }



    }
}
