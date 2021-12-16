using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.Repositories
{
    public interface IRepositoryClients
    {
        List<Client> GetAll();
        void AddClient(Client _Client);
        void DeleteClient(int _Client);
        void ReadmitClient(int _Client);
    }
}
