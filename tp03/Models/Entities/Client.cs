using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class Client
    {
        private string dni;
        private int id;
        private string name;
        private string address;
        private string phoneNum;

        public string Dni { get => dni; set => dni = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }

        public Client() { }

        public Client(string dni, int id, string name, string address, string phoneNum)
        {
            Dni = dni;
            Id = id;
            Name = name;
            Address = address;
            PhoneNum = phoneNum;
        }
    }
}
