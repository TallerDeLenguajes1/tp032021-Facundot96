using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.Repositories
{
    public interface IRepositoryUsers
    {
        List<User> GetAll();
        User StartLogin(string _Username, string _Password);
        List<MenuOptions> GetOptions(int id);
        void AddUser(User _User);
        string GetCode(int _Code);
    }
}
