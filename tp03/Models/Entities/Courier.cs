using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class Courier
    {
        private string courierId;
        private string courierName;

        public string CourierId { get => courierId; set => courierId = value; }
        public string CourierName { get => courierName; set => courierName = value; }

        public Courier(){   }

        public Courier(string courierId, string courierName)
        {
            CourierId = courierId;
            CourierName = courierName;
        }
    }
}
