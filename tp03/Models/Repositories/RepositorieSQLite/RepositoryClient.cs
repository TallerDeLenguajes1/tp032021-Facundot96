using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using tp03.Models.Entities;

namespace tp03.Models.Repositories.RepositorieSQLite
{
    public class RepositoryClient : IRepositoryClients
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositoryClient(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        public List<Client> GetAll()
        {
            List<Client> ClientList = new List<Client>();
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Clientes;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Client client = new Client()
                        {
                            Id = (int)DataReader["clientID"],
                            Name = DataReader["clienteNombre"].ToString(),
                            PhoneNum = DataReader["clienteTelefono"].ToString(),
                            Address = DataReader["clienteDireccion"].ToString()
                        };
                        ClientList.Add(client);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return ClientList;
        }

        public void AddClient(Client _Client)
        {
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = @"INSERT INTO Clientes SET clienteNombre ='" + _Client.Name + "', clienteTelefono ='" + _Client.PhoneNum + "', clienteDireccion ='" + _Client.Address + "', clienteEstado = 1;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    conection.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
        }
        public void ReadmitClient(int _ClientCode)
        {
            executeQueryStatusClient(_ClientCode, 1);
        }
        public void DeleteClient(int _ClientCode)
        {
            executeQueryStatusClient(_ClientCode, 0);
        }

        public void executeQueryStatusClient(int _ClientCode, int _Status)
        {
            try
            {
                Client ClienteChoosen = new Client();
                string SQLQuery = "UPDATE Clientes SET clienteEstado= @estado WHERE clienteID = @clienteCodigo;";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@estado", _Status);
                        command.Parameters.AddWithValue("@clienteCodigo", _ClientCode);
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
    }
}
