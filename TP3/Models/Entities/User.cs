using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP3.Models.Entities
{
    public class User
    {
        private int id;
        private string name;
        private string password;
        private string email;
        private int userType;

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Email { get => email; set => email = value; }
        public int UserType { get => userType; set => userType = value; }

        public User()
        {

        }

        public User(int id, string name, string password, string email)
        {
            Id = id;
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
