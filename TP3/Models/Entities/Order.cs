using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Models.Entities
{
    public class Order
    {
        private int number;
        private string observation;
        private int clientId;
        private string status;
        private int deliverId;
        private string adress;

        public int Number { get => number; set => number = value; }
        public string Observation { get => observation; set => observation = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public string Status { get => status; set => status = value; }
        public int DeliverId { get => deliverId; set => deliverId = value; }
        public string Adress { get => adress; set => adress = value; }

        public Order() { 
        
        }

        public Order(int number, string observation, int clientId, string status, int deliverId, string adress)
        {
            Number = number;
            Observation = observation;
            ClientId = clientId;
            Status = status;
            DeliverId = 0;
            Adress = adress;
        }

    }
}
