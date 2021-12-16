using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.ViewModels;

namespace tp03.Models.Repositories
{
    public interface IRepositoryDeliveryMs
    {
        List<DeliveryM> GetAll();
        DeliveryM GetOne(int _DeliveryMCode, int _UserCode);
        DeliveryMViewModel GetOneCourier(int _DeliveryMCode, DataContext _Db);
        void AddDeliveryM(DeliveryM _DeliveryM);
        void ModifyDeliveryM(DeliveryM _DeliveryM);
        void ReadmitDeliveryM(int _DeliveryMCode);
        void DeleteDeliveryM(int _DeliveryMCode);
    }
}
