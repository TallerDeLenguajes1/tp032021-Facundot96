using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;
using tp03.Models.ViewModels;

namespace tp03.Models.Repositories.RepositorieSQLite
{
    public class RepositoryDeliveryM : IRepositoryDeliveryMs
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositoryDeliveryM(string _ConnectionString,Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        public List<DeliveryM> GetAll()
        {
            List<DeliveryM> ListDeliveryM = new List<DeliveryM>();
            try
            {
                using(SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT* FROM Cadetes;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        DeliveryM deliveryM = new DeliveryM()
                        {
                            Id = Convert.ToInt32(DataReader["cadeteID"]),
                            Name = DataReader["cadeteNombre"].ToString(),
                            PhoneNum = DataReader["cadeteTelefono"].ToString(),
                            Address = DataReader["cadeteDireccion"].ToString(),
                            CourierId = DataReader["cadeteriaID"].ToString(),
                            Active = Convert.ToInt32(DataReader["cadeteActivo"]),
                            OrdersActive = Convert.ToInt32(DataReader["cadetePedidosActivos"]),
                            OrdersComplete = Convert.ToInt32(DataReader["cadetePedidosRealizados"])
                        };
                        ListDeliveryM.Add(deliveryM);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            return ListDeliveryM;
        }

        public DeliveryM GetOne(int _DeliveryMCode, int _UserCode)
        {
            DeliveryM deliveryChoosen = new DeliveryM();
            int _CodeToUse = 0;
            string SQLQuery = "";
            try
            {
                if(_UserCode != 0)
                {
                    _CodeToUse = _UserCode;
                    SQLQuery = @"SELECT * FROM Cadetes WHERE usuarioID = @Codigo";
                }
                else
                {
                    _CodeToUse = _DeliveryMCode;
                    SQLQuery = @"SELECT * FROM Cadetes WHERE cadeteID = @Codigo";
                }
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using(SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        conection.Open();
                        command.Parameters.AddWithValue("@codigo", _CodeToUse);
                        SQLiteDataReader DataReader = command.ExecuteReader();
                        if (DataReader.Read())
                        {
                            deliveryChoosen.Id = Convert.ToInt32(DataReader["cadeteID"]);
                            deliveryChoosen.OrdersComplete = Convert.ToInt32(DataReader["cadetePedidosRealizados"]);
                            deliveryChoosen.OrdersActive = Convert.ToInt32(DataReader["cadetePedidosActivos"]);
                            deliveryChoosen.UserId = Convert.ToInt32(DataReader["usuarioID"]);
                            deliveryChoosen.Name = DataReader["cadeteNombre"].ToString();
                            deliveryChoosen.PhoneNum = DataReader["cadeteTelefono"].ToString();
                            deliveryChoosen.Address = DataReader["cadeteDireccion"].ToString();
                            deliveryChoosen.CourierId = DataReader["cadeteriaID"].ToString();
                        }
                        DataReader.Close();
                        conection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            return deliveryChoosen;
        }

        public DeliveryMViewModel GetOneCourier(int _DeliveryMCode, DataContext _Db)
        {
            DeliveryMViewModel deliveryChoosen = new DeliveryMViewModel();
            deliveryChoosen.DeliveryM = GetOne(_DeliveryMCode, 0);
            deliveryChoosen.CourierList = _Db.Couriers.GetAll();
            return deliveryChoosen;
        }

        public void AddDeliveryM(DeliveryM _DeliveryM)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Cadetes (cadeteNombre, cadeteTelefono, cadeteDireccion, cadeteriaID) VALUES (@cadeteNombre, @cadeteTelefono, @cadeteDireccion, @cadeteCadeteriaID)";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@cadeteNombre", _DeliveryM.Name);
                        command.Parameters.AddWithValue("@cadeteTelefono", _DeliveryM.PhoneNum);
                        command.Parameters.AddWithValue("@cadeteDireccion", _DeliveryM.Address);
                        command.Parameters.AddWithValue("@cadeteCadeteriaID", _DeliveryM.CourierId);
                        conection.Open();
                        command.ExecuteNonQuery();
                        conection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
        }
        public void ModifyDeliveryM(DeliveryM _DeliveryM)
        {
            try
            {
                string SQLQuery = @"UPDATE Cadetes SET cadeteNombre = @cadeteNombre, cadeteTelefono = @cadeteTelefono, cadeteDireccion = @cadeteDireccion, cadeteriaID = @cadeteCadeteriaID WHERE cadeteID = @cadeteCodigo";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@cadeteNombre", _DeliveryM.Name);
                        command.Parameters.AddWithValue("@cadeteTelefono", _DeliveryM.PhoneNum);
                        command.Parameters.AddWithValue("@cadeteDireccion", _DeliveryM.Address);
                        command.Parameters.AddWithValue("@cadeteCadeteriaID", _DeliveryM.CourierId);
                        command.Parameters.AddWithValue("@cadeteCodigo", _DeliveryM.Id);
                        conection.Open();
                        command.ExecuteNonQuery();
                        conection.Close();
                    }
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
        }

        public void ReadmitDeliveryM(int _DeliveryMCode)
        {
            executeQueryStatus(_DeliveryMCode,1);
        }

        public void DeleteDeliveryM(int _DeliveryMCode)
        {
            executeQueryStatus(_DeliveryMCode, 0);
        }

        public void executeQueryStatus(int _DeliveryMCode, int _Status)
        {
            try
            {
                string SQLQuery = @"UPDATE Cadetes SET cadeteActivo = @cadeteActivo WHERE cadeteID = @cadeteID;";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@cadeteActivo", _Status);
                        command.Parameters.AddWithValue("@cadeteID", _DeliveryMCode);
                        conection.Open();
                        command.ExecuteNonQuery();
                        conection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }
    }
}
