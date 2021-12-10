using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;
using TP3.Models.ViewModels;

namespace TP3.Models.Repositories.RepositoriesSQLite
{
    public class RepositorieDeliveryM : IRepositorieDeliveryM
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorieDeliveryM(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        void IRepositorieDeliveryM.AddDeliveryM(DeliveryM _DeliveryM)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Cadetes (cadeteNombre, cadeteTelefono, cadeteDireccion, cadeteriaID) VALUES (@cadeteNombre, @cadeteTelefono, @cadeteDireccion, @cadeteCadeteriaID)";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteNombre", _DeliveryM.Name);
                        command.Parameters.AddWithValue("@cadeteTelefono", _DeliveryM.PhoneNum);
                        command.Parameters.AddWithValue("@cadeteDireccion", _DeliveryM.Adress);
                        command.Parameters.AddWithValue("@cadeteCadeteriaID", _DeliveryM.Courier);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        void IRepositorieDeliveryM.DeleteDeliveryM(int _DeliverId)
        {
            executeQueryEstado(_DeliverId, 0);
        }

        List<DeliveryM> IRepositorieDeliveryM.getAll()
        {
            List<DeliveryM> DeliveryMList = new List<DeliveryM>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM Cadetes;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        DeliveryM cadete = new DeliveryM()
                        {
                            Id = Convert.ToInt32(DataReader["cadeteID"]),
                            Name = DataReader["cadeteNombre"].ToString(),
                            PhoneNum = DataReader["cadeteTelefono"].ToString(),
                            Adress = DataReader["cadeteDireccion"].ToString(),
                            Courier = DataReader["cadeteriaID"].ToString(),
                            Active = Convert.ToInt32(DataReader["cadeteActivo"]),
                            OrdersActives = Convert.ToInt32(DataReader["cadetePedidosActivos"]),
                            OrdersComplete = Convert.ToInt32(DataReader["cadetePedidosRealizados"])
                        };
                        DeliveryMList.Add(cadete);
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return DeliveryMList;
        }

        public DeliveryM GetOne(int _DeliverId, int _DB)
        {
            DeliveryM DeliveryMC = new DeliveryM();
            try
            {
                string SQLQuery = @"SELECT * FROM Cadetes WHERE cadeteID = " + _DeliverId + ";";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        conexion.Open();
                        SQLiteDataReader DataReader = command.ExecuteReader();
                        if (DataReader.Read())
                        {
                            DeliveryMC.Id = Convert.ToInt32(DataReader["cadeteID"]);
                            DeliveryMC.Name = DataReader["cadeteNombre"].ToString();
                            DeliveryMC.PhoneNum = DataReader["cadeteTelefono"].ToString();
                            DeliveryMC.Adress = DataReader["cadeteDireccion"].ToString();
                            DeliveryMC.Courier = DataReader["cadeteriaID"].ToString();
                            DeliveryMC.OrdersActives = Convert.ToInt32(DataReader["cadetePedidosRealizados"]);
                            DeliveryMC.OrdersComplete = Convert.ToInt32(DataReader["cadetePedidosActivos"]);
                        }
                        DataReader.Close();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return DeliveryMC;
        }

        DeliveryMViewModel IRepositorieDeliveryM.GetOneCourier(int _DeliverId, DataContext _DB)
        {
            DeliveryMViewModel cadeteElegido = new DeliveryMViewModel();
            cadeteElegido.DeliveryM = GetOne(_DeliverId, 0);
            cadeteElegido.CourierList = _DB.Courier.getAll();
            return cadeteElegido;
        }

        void IRepositorieDeliveryM.ModifyDeliveryM(DeliveryM _DeliveryM)
        {
            try
            {
                string SQLQuery = @"UPDATE Cadetes SET cadeteNombre = @cadeteNombre, cadeteTelefono = @cadeteTelefono, cadeteDireccion = @cadeteDireccion, cadeteriaID = @cadeteCadeteriaID WHERE cadeteID = @cadeteCodigo";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteNombre", _DeliveryM.Name);
                        command.Parameters.AddWithValue("@cadeteTelefono", _DeliveryM.PhoneNum);
                        command.Parameters.AddWithValue("@cadeteDireccion", _DeliveryM.Adress);
                        command.Parameters.AddWithValue("@cadeteCadeteriaID", _DeliveryM.Courier);
                        command.Parameters.AddWithValue("@cadeteCodigo", _DeliveryM.Id);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        void IRepositorieDeliveryM.ReadmitDeliveryM(int _DeliverId)
        {
            executeQueryEstado(_DeliverId, 1);
        }

        public void executeQueryEstado(int _CadeteCodigo, int _Estado)
        {
            try
            {
                string SQLQuery = @"UPDATE Cadetes SET cadeteActivo = @cadeteActivo WHERE cadeteID = @cadeteID;";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteActivo", _Estado);
                        command.Parameters.AddWithValue("@cadeteID", _CadeteCodigo);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
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
