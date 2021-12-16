using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class DeliveryM
    {
        private int id;
        private int ordersComplete;
        private int ordersActive;
        private int active;
        private int userId;
        private string name;
        private string address;
        private string phoneNum;
        private string courierId;
        private List<Order> orderList;

        
        public int OrdersComplete { get => ordersComplete; set => ordersComplete = value; }
        public int OrdersActive { get => ordersActive; set => ordersActive = value; }
        public int Active { get => active; set => active = value; }
        public int UserId { get => userId; set => userId = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string CourierId { get => courierId; set => courierId = value; }
        public List<Order> OrderList { get => orderList; set => orderList = value; }
        public int Id { get => id; set => id = value; }

        public DeliveryM()
        {
            OrderList = new List<Order>();
        }

        public DeliveryM(int id, string name, string address, string phoneNum)
        {
            Id = id;
            Name = name;
            Address = address;
            PhoneNum = phoneNum;
            OrderList = new List<Order>();
            Active = 0;
        }
    }
}
