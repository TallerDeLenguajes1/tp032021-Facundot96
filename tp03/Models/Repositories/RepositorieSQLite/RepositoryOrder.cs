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
    public class RepositoryOrder : IRepositoryOrders
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositoryOrder(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        public List<Order> GetAll()
        {
            List<Order> OrderList = new List<Order>();
            try
            {
                using(SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Pedidos;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Number= Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            Address = DataReader["pedidoDireccion"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            DeliverCode = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        OrderList.Add(order);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            return OrderList;
        }

        public void AddOrder(Order _Order)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Pedidos (pedidoObservacion, clienteID, pedidoDireccion) VALUES (@pedidoObservacion,  @ClienteID, @pedidoDireccion)";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@pedidoObservacion", _Order.Observation);
                        command.Parameters.AddWithValue("@ClienteID", _Order.ClientId);
                        command.Parameters.AddWithValue("@pedidoDireccion", _Order.Address);
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

        public void AddOrderDeliveryM(Order _Order, DeliveryM _DeliveryM)
        {
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = @"UPDATE Pedidos SET cadeteId= @cadeteId, estadoPedido= 'En camino' WHERE pedidoID = @pedidoID;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@cadeteId", _Order.DeliverCode);
                        command.Parameters.AddWithValue("@pedidoID", _Order.Number);
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

        public void CancelOrder(int _DeliverCode)
        {
            executeTaskOrder(_DeliverCode, "Cancelado");
        }
        public void FinishOrder(int _DeliverCode)
        {
            executeTaskOrder(_DeliverCode, "Completado");
        }

        public void AcceptOrder(int _DeliverCode, int Code)
        {
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = @"UPDATE Pedidos SET pedidoEstado= @pedidoEstado, cadeteID= @cadeteID WHERE pedidoID = @pedidoID;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@pedidoEstado", "En Camino");
                        command.Parameters.AddWithValue("@pedidoID", _DeliverCode);
                        command.Parameters.AddWithValue("@cadeteID", Code);
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
        public List<Order> GetAllOrdersDeliveryM(int _Id)
        {
            List<Order> OrderList = new List<Order>();
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Pedidos WHERE cadeteID = @cadeteID;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    command.Parameters.AddWithValue("@cadeteID", _Id);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Number = Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            Address = DataReader["pedidoDireccion"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            DeliverCode = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        OrderList.Add(order);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return OrderList;
        }
        public void executeTaskOrder(int _DeliverCode, string _Status)
        {
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = @"UPDATE Pedidos SET pedidoEstado= @pedidoEstado WHERE pedidoID = @pedidoID;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@pedidoEstado", _Status);
                        command.Parameters.AddWithValue("@pedidoID", _DeliverCode);
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
        public OrderViewModel GetOneDeliveryMClient(DataContext _DB)
        {
            OrderViewModel deliveryMChoosen = new OrderViewModel();
            deliveryMChoosen.DeliveryM = _DB.DeliveryMs.GetAll();
            deliveryMChoosen.Order = new Order();
            return deliveryMChoosen;
        }
        public List<Order> GetAllOrdersClient(int _ClientCode)
        {
            List<Order> OrderList = new List<Order>();
            try {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = @"SELECT * FROM Pedidos WHERE ClienteID = @ClienteID;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    command.Parameters.AddWithValue("@ClienteID", _ClientCode);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Number = Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            Address = DataReader["pedidoDireccion"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            DeliverCode = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        OrderList.Add(order);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return OrderList;
        }

        public List<Order> GetAllAvailable()
        {
            List<Order> OrderList = new List<Order>();
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Pedidos WHERE CadeteID = 0;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Number = Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            Address = DataReader["pedidoDireccion"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            DeliverCode = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        OrderList.Add(order);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return OrderList;
        }
        public void cancelOrder(int _Id)
        {
            try
            {
                string SQLQuery = @"UPDATE Pedidos SET pedidoEstado = @pedidoEstado, pedidoActivo = 0 WHERE pedidoID = @pedidoID;";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@pedidoID", _Id);
                        command.Parameters.AddWithValue("@pedidoEstado", "Cancelado");
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
