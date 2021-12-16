using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tp03.Models.Entities
{
    public class User
    {
        private string userId;
        private string name;
        private int type;
        private string email;
        private string password;
        private string code;

        public string UserId { get => userId; set => userId = value; }
        public string Name { get => name; set => name = value; }
        public int Type { get => type; set => type = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string Code { get => code; set => code = value; }

        public User() { }
    }
}
