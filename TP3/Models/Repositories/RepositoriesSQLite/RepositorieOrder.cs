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
    public class RepositorieOrder : IRepositorieOrder
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorieOrder(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }
        public List<Order> getAll()
        {
            List<Order> OrderList = new List<Order>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM Pedidos;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order order = new Order()
                        {
                            Number = Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            Adress = DataReader["pedidoDireccion"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            DeliverId = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        OrderList.Add(order);
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return OrderList;
        }
        public void AddOrder(Order _Pedido)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Pedidos (pedidoObservacion, clienteID) VALUES (@pedidoObservacion,  @ClienteID)";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@pedidoObservacion", _Pedido.Observation);
                        command.Parameters.AddWithValue("@ClienteID", _Pedido.ClientId);
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

        public void AddOrderDeliver(Order _Pedido, DeliveryM _Cadete)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = @"UPDATE Pedidos SET cadeteId= @cadeteId, estadoPedido= 'En camino' WHERE pedidoID = @pedidoID;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@cadeteId", _Pedido.DeliverId);
                        command.Parameters.AddWithValue("@pedidoID", _Pedido.Number);
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

        public void CancelOrder(Order _Pedido)
        {
            executeTaskPedido(_Pedido, "Cancelado");
        }
        public void FinishOrder(Order _Pedido)
        {
            executeTaskPedido(_Pedido, "Completado");
        }

        public void executeTaskPedido(Order _Pedido, string _Accion)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    string SQLQuery = @"UPDATE Pedidos SET pedidoEstado= @Accion WHERE pedidoID = @pedidoID;";
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@pedidoEstado", _Accion);
                        command.Parameters.AddWithValue("@pedidoID", _Pedido.Number);
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
        public OrderViewModel GetOneDeliverUser(DataContext _DB)
        {
            OrderViewModel cadeteElegido = new OrderViewModel();
            cadeteElegido.DeliveryM = _DB.DeliveryM.getAll();
            cadeteElegido.Order = new Order();
            return cadeteElegido;
        }
        public List<Order> GetAllOrdersClient(int _CodigoCliente)
        {
            List<Order> ListadoPedidos = new List<Order>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = @"SELECT * FROM Pedidos WHERE ClienteID = @ClienteID;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    command.Parameters.AddWithValue("@ClienteID", _CodigoCliente);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Order pedido = new Order()
                        {
                            Number = Convert.ToInt32(DataReader["pedidoID"]),
                            Observation = DataReader["pedidoObservacion"].ToString(),
                            Status = DataReader["pedidoEstado"].ToString(),
                            ClientId = Convert.ToInt32(DataReader["clienteID"]),
                            Adress = DataReader["pedidoDireccion"].ToString(),
                            DeliverId = Convert.ToInt32(DataReader["cadeteID"])
                        };
                        ListadoPedidos.Add(pedido);
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return ListadoPedidos;
        }
        public void CancelOrder(int _Id)
        {
            try
            {
                string SQLQuery = @"UPDATE Pedidos SET pedidoEstado = @pedidoEstado WHERE pedidoID = @pedidoID;";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@pedidoID", _Id);
                        command.Parameters.AddWithValue("@pedidoEstado", "Cancelado");
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
