using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;

namespace TP3.Models.Repositories.RepositoriesSQLite
{
    public class RepositorieClient : IRepositorieClient
    {

        private readonly string connectionString;
        private readonly Logger log;

        public RepositorieClient(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }
        public void AddClient(Client _Client)
        {
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = @"INSERT INTO Clientes SET clienteNombre ='" + _Client.Name + "', clienteTelefono ='" + _Client.PhoneNum + "', clienteDireccion ='" + _Client.Adress + "', clienteEstado = 1 ;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }

        public void DeleteClient(int _ClientId)
        {
            executeQueryEstadoCliente(_ClientId, 0);
        }

        public List<Client> getAll()
        {
            List<Client> ListadoClientes = new List<Client>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM Clientes;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Client client = new Client()
                        {
                            Id = (int)DataReader["clienteID"],
                            Name = DataReader["clienteNombre"].ToString(),
                            PhoneNum = DataReader["clienteTelefono"].ToString(),
                            Adress = DataReader["clienteDireccion"].ToString()
                        };
                        ListadoClientes.Add(client);
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return ListadoClientes;
        }

        public void ReadmitClient(int _ClientId)
        {
            executeQueryEstadoCliente(_ClientId, 1);
        }

        public void executeQueryEstadoCliente(int _ClienteCodigo, int _Estado)
        {
            try
            {
                Client ClienteSelecionado = new Client();
                string SQLQuery = "UPDATE Clientes SET clienteEstado= @estado WHERE clienteID = @clienteCodigo;";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@estado", _Estado);
                        command.Parameters.AddWithValue("@clienteCodigo", _ClienteCodigo);
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
