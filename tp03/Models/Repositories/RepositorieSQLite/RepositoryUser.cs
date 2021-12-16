using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using tp03.Models.Entities;

namespace tp03.Models.Repositories.RepositorieSQLite
{
    public class RepositoryUser : IRepositoryUsers
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositoryUser(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }

        public List<User> GetAll()
        {
            List<User> UserList = new List<User>();
            try
            {
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    conection.Open();
                    string SQLQuery = "SELECT usuarioID, usuarioNombre, usuarioClearance, usuarioEmail FROM Usuarios;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conection);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read()) 
                    {
                        User user = new User()
                        {
                            UserId = DataReader["usuarioID"].ToString(),
                            Name = DataReader["usuarioNombre"].ToString(),
                            Email = DataReader["usuarioEmail"].ToString(),
                            Type = Convert.ToInt32(DataReader["usuarioClearance"])
                        };
                        UserList.Add(user);
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            return UserList;
        }
        public void AddUser(User _User)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Usuarios (usuarioNombre, usuarioPassword, usuarioEmail) VALUES (@usuarioNombre, @usuarioPassword, @usuarioEmail)";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@usuarioNombre",_User.Name);
                        command.Parameters.AddWithValue("@usuarioPassword", _User.Password);
                        command.Parameters.AddWithValue("@usuarioEmail", _User.Email);
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
        public User StartLogin(string _Username, string _Password)
        {
            User user = new User()
            {
                UserId = "",
                Name = "",
                Type = 0
            };
            try
            {
                string SQLQuery = @"SELECT usuarioID, usuarioNombre, usuarioClearance FROM Usuarios WHERE usuarioNombre = @username AND usuarioPassword = @contrasena;";
                using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                    {
                        command.Parameters.AddWithValue("@username", _Username);
                        command.Parameters.AddWithValue("@contrasena", _Password);
                        conection.Open();
                        SQLiteDataReader DataReader = command.ExecuteReader();
                        if (DataReader.Read())
                        {
                            user.UserId = DataReader["usuarioID"].ToString();
                            user.Name = DataReader["usuarioNombre"].ToString();
                            user.Type = Convert.ToInt32(DataReader["usuarioClearance"]);
                        }
                        DataReader.Close();
                        conection.Close();
                    }
                }
                user.Code = GetCode(Convert.ToInt32(user.UserId));
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
            }
            return user;
        }

        public string GetCode(int _Code)
        {
            string code = "";
            string SQLQuery = @"SELECT cadeteID FROM Cadetes WHERE usuarioID = @usuarioID";
            using (SQLiteConnection conection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                {
                    var asd = command.Parameters.AddWithValue("@usuarioID", _Code);
                    conection.Open();
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    if (DataReader.Read())
                    {
                        code = DataReader["cadeteID"].ToString();
                    }
                    DataReader.Close();
                    conection.Close();
                }
            }
            return code;
        }

        public List<MenuOptions> GetOptions(int _Type)
        {
            List<MenuOptions> options = new List<MenuOptions>();
            try
            {
                if (_Type != 0)
                {
                    string SQLQuery = @"SELECT opcionURL, opcionNombre, opcionControlador FROM Opciones WHERE opcionClearance = @opcionClearance;";
                    using (SQLiteConnection conection = new SQLiteConnection(connectionString))
                    {
                        using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conection))
                        {
                            command.Parameters.AddWithValue("@opcionClearance", _Type);
                            conection.Open();
                            SQLiteDataReader DataReader = command.ExecuteReader();
                            while (DataReader.Read())
                            {
                                MenuOptions option = new MenuOptions
                                {
                                    Url = DataReader["opcionURL"].ToString(),
                                    Name = DataReader["opcionNombre"].ToString(),
                                    Controller = DataReader["opcionControlador"].ToString()
                                };
                                options.Add(option);
                            }
                            DataReader.Close();
                            conection.Close();
                        }
                    }
                }
                else
                {
                    var option = new MenuOptions()
                    {
                        Type = 0,
                        Url = "Index",
                        Controller = "Home",
                        Name = "Ingresar"
                    };
                    options.Add(option);
                }
            }
            catch(Exception ex)
            {
                string error = ex.ToString();
                var option = new MenuOptions()
                {
                    Type = 0,
                    Url = "Index",
                    Controller = "Home",
                    Name = "Ingresar"
                };
                options.Add(option);
            }
            return options;
        }
    }
}
