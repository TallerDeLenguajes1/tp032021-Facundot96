using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;

namespace TP3.Models.Repositories
{
    public interface IRepositorieClient
    {
        List<Client> getAll();
        void AddClient(Client _Client);
        void DeleteClient(int _Client);
        void ReadmitClient(int _Client);
    }
}
