using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;
using TP3.Models.ViewModels;

namespace TP3.Models.Repositories
{
    public interface IRepositorieDeliveryM
    {
        List<DeliveryM> getAll();
        DeliveryM GetOne(int _DeliverId, int _DB);
        DeliveryMViewModel GetOneCourier(int _DeliverId, DataContext _DB);
        void AddDeliveryM(DeliveryM _DeliveryM);
        void ModifyDeliveryM(DeliveryM _DeliveryM);
        void ReadmitDeliveryM(int _DeliverId);
        void DeleteDeliveryM(int _DeliverId);

    }
}
