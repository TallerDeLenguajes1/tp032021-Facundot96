using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemEntities;
using TP3.Models.Entities;

namespace TP3.Models.Repositories
{
    public interface IRepositorieUser
    {
        List<User> GetAll();
        User StartLogin(string _Username, string _Password);
        List<MenuOptions> ObtainOptions(int id);
        void AddUser(User _User);
    }
}
