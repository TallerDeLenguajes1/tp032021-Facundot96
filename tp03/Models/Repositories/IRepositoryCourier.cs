using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.Repositories
{
    public interface IRepositoryCouriers
    {
        List<Courier> GetAll();
    }
}
