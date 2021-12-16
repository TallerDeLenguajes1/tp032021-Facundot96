using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class Order
    {
        private int number;
        private int clientId;
        private int deliverCode;
        private string observation;
        private string status;
        private string address;

        public int Number { get => number; set => number = value; }
        public int ClientId { get => clientId; set => clientId = value; }
        public int DeliverCode { get => deliverCode; set => deliverCode = value; }
        public string Observation { get => observation; set => observation = value; }
        public string Status { get => status; set => status = value; }
        public string Address { get => address; set => address = value; }

        public Order() { }

        public Order(int number, int clientId , int deliverCode, string observation , string status , string address )
        {
            Number = number;
            ClientId = clientId;
            DeliverCode = 0;
            Observation = observation;
            Status = status;
            Address = address;
        }

    }
}
