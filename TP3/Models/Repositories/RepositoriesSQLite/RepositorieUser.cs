using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using SystemEntities;
using TP3.Models.Entities;

namespace TP3.Models.Repositories.RepositoriesSQLite
{
    public class RepositorieUser
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorieUser(string _ConnectionString, Logger log)
        {
            this.connectionString = _ConnectionString;
            this.log = log;
        }
        public List<User> GetAll()
        {
            List<User> UsersList = new List<User>();
            try
            {
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    conexion.Open();
                    string SQLQuery = "SELECT usuarioID, usuarioNombre, usuarioClearance, usuarioEmail FROM Usuarios;";
                    SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion);
                    SQLiteDataReader DataReader = command.ExecuteReader();
                    while (DataReader.Read())
                    {
                        User usuario = new User()
                        {
                            Id = Convert.ToInt32(DataReader["usuarioID"]),
                            Name = DataReader["usuarioNombre"].ToString(),
                            Email = DataReader["usuarioEmail"].ToString(),
                            UserType = Convert.ToInt32(DataReader["usuarioClearance"])
                        };
                        UsersList.Add(usuario);
                    }
                    DataReader.Close();
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
            }
            return UsersList;
        }
        public void AddUser(User _Usuario)
        {
            try
            {
                string SQLQuery = @"INSERT INTO Usuarios (usuarioNombre, usuarioPassword, usuarioEmail) VALUES (@usuarioNombre, @usuarioPassword, @usuarioEmail)";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@usuarioNombre", _Usuario.Name);
                        command.Parameters.AddWithValue("@usuarioPassword", _Usuario.Password);
                        command.Parameters.AddWithValue("@usuarioEmail", _Usuario.Email);
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
        public User StartLogin(string _Username, string _Contrasena)
        {
            User usuario = new User()
            {
                Id = 0,
                Name = "",
                UserType = 0
            };
            try
            {
                string SQLQuery = @"SELECT usuarioID, usuarioNombre, usuarioClearance FROM Usuarios WHERE usuarioNombre = @username AND usuarioPassword = @contrasena;";
                using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                    {
                        command.Parameters.AddWithValue("@username", _Username);
                        command.Parameters.AddWithValue("@contrasena", _Contrasena);
                        conexion.Open();
                        SQLiteDataReader DataReader = command.ExecuteReader();
                        if (DataReader.Read())
                        {
                            usuario.Id = Convert.ToInt32(DataReader["usuarioID"]);
                            usuario.Name = DataReader["usuarioNombre"].ToString();
                            usuario.UserType = Convert.ToInt32(DataReader["usuarioClearance"]);
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
            return usuario;
        }

        public List<MenuOptions> ObtainOptions(int _Clearance)
        {
            List<MenuOptions> options = new List<MenuOptions>();
            try
            {
                if (_Clearance != 0)
                {
                    string SQLQuery = @"SELECT opcionURL, opcionNombre, opcionControlador FROM Opciones WHERE opcionClearance = @opcionClearance;";
                    using (SQLiteConnection conexion = new SQLiteConnection(connectionString))
                    {
                        using (SQLiteCommand command = new SQLiteCommand(SQLQuery, conexion))
                        {
                            command.Parameters.AddWithValue("@opcionClearance", _Clearance);
                            conexion.Open();
                            SQLiteDataReader DataReader = command.ExecuteReader();
                            while (DataReader.Read())
                            {
                                MenuOptions opcion = new MenuOptions
                                {
                                    Url = DataReader["opcionURL"].ToString(),
                                    Name = DataReader["opcionNombre"].ToString(),
                                    Control1 = DataReader["opcionControlador"].ToString()
                                };
                                options.Add(opcion);
                            }
                            DataReader.Close();
                            conexion.Close();
                        }
                    }
                }
                else
                {///VER////
                    var opcion = new MenuOptions()
                    {
                        UserType = 0,
                        Url = "Index",
                        Control1 = "Home",
                        Name = "Ingresar"
                    };
                    options.Add(opcion);
                }
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                var opcion = new MenuOptions()
                {
                    UserType = 0,
                    Url = "Index",
                    Control1 = "Home",
                    Name = "Ingresar"
                };
                options.Add(opcion);
            }
            return options;
        }
    }
}

