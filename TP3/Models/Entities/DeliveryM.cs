using System.Collections.Generic;

namespace TP3.Models.Entities
{
    public class DeliveryM
    {
        private int id;
        private string name;
        private string adress;
        private string phoneNum;
        private List<Order> orders;
        private string courier;
        private int ordersComplete;
        private int ordersActives;
        private int active;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Adress { get => adress; set => adress = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public List<Order> Orders { get => orders; set => orders = value; }
        public string Courier { get => courier; set => courier = value; }
        public int OrdersComplete { get => ordersComplete; set => ordersComplete = value; }
        public int OrdersActives { get => ordersActives; set => ordersActives = value; }
        public int Active { get => active; set => active = value; }

        public DeliveryM()
        {
            Orders = new List<Order>();
        }

        public DeliveryM(string name, string adress, string phoneNum, int id, int active)
        {
            Name = name;
            Adress = adress;
            PhoneNum = phoneNum;
            Orders = new List<Order>();
            Id = id;
            Active = 1;
        }

        public void AddOrder(Order newOrder)
        {
            Orders.Add(newOrder);
        }

        public void RemoveOrder(Order ord)
        {
            Orders.Remove(ord);
        }

    }
}
