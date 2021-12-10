using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;

namespace TP3.Models.Repositories
{
    public interface IRepositorieCourier
    {
        List<Courier> getAll();
    }
}
