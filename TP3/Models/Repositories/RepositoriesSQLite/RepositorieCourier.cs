using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TP3.Models.Entities;

namespace TP3.Models.Repositories.RepositoriesSQLite
{
    public class RepositorieCourier : IRepositorieCourier
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorieCourier(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }
        List<Courier> IRepositorieCourier.getAll()
        {
            List<Courier> CourierList = new List<Courier>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT * FROM Cadeterias;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Courier cadeteria = new Courier()
                        {
                            CourierId = DataReader["cadeteriaID"].ToString(),
                            CourierName = DataReader["cadeteriaNombre"].ToString()
                        };
                        CourierList.Add(cadeteria);
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return CourierList;
        }
    }
 }

