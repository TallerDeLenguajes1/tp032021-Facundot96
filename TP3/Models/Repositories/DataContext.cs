using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Repositories.RepositoriesSQLite;

namespace TP3.Models.Repositories
{
    public class DataContext
    {
        public IRepositorieClient Client { get; set; }
        public IRepositorieCourier Courier { get; set; }
        public IRepositorieDeliveryM DeliveryM { get; set; }
        public IRepositorieOrder Order { get; set; }
        public IRepositorieUser User { get; set; }

        public DataContext(IRepositorieClient client, IRepositorieCourier courier, IRepositorieDeliveryM deliveryM, IRepositorieOrder order, IRepositorieUser user)
        {
            Client = client;
            Courier = courier;
            DeliveryM = deliveryM;
            Order = order;
            User = user;
        }

        public DataContext(RepositorieClient repoClient, RepositorieCourier repoCourier, RepositorieDeliveryM repoDeliverM, RepositorieOrder repoOrder, RepositorieUser repoUser)
        {
        }
    }
}
