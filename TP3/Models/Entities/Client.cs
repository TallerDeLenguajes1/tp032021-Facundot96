using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Models.Entities
{
    public class Client
    {

        private int id;
        private int dni;
        private string name;
        private string adress;
        private string phoneNum;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Adress { get => adress; set => adress = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public int Dni { get => dni; set => dni = value; }

        public Client()
        {

        }

        public Client(string name, string adress, string phoneNum, int dni)
        {
            Name = name;
            Adress = adress;
            PhoneNum = phoneNum;
            Dni = dni;
        }
    }
}
