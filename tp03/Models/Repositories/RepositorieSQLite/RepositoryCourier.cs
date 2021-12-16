using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.Repositories.RepositorieSQLite
{
    public class RepositoryCourier : IRepositoryCouriers
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositoryCourier(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        public List<Courier> GetAll()
        {
            List<Courier> CourierList = new List<Courier>();
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT * FROM Cadeterias;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        Courier courier = new Courier()
                        {
                            CourierId = DataReader["cadeteriaID"].ToString(),
                            CourierName = DataReader["cadeteriaNombre"].ToString()
                        };
                        CourierList.Add(courier);
                    }
                    conection.Close();
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }

            return CourierList;
        }
    }

}
