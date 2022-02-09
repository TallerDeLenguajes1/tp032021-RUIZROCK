using CadeteriaWeb.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorioUsuario(string connection, Logger log)
        {
            this.connectionString = connection;
            this.log = log;
        }

        // -----------------------------------------------------------------
        // -------------------------DELETE USUARIO--------------------------
        // -----------------------------------------------------------------
        public void DeleteUsuario(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Usuarios SET usuarioAlta = 0 WHERE usuarioID = @id;";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------INSERT USUARIO--------------------------
        // -----------------------------------------------------------------
        public void InsertUsuario(Usuario item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Usuarios(usuarioName, usuarioPass, usuarioRol, usuarioAlta)" +
                    $" VALUES(@username, @password, 'USER', 1);";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@username", item.Username);
                command.Parameters.AddWithValue("@password", item.Password);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------UPDATE USUARIO--------------------------
        // -----------------------------------------------------------------
        public void UpdateUsuario(Usuario item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Usuarios" +
                    $" SET usuarioName = @username, usuarioPass = @password" +
                    $" WHERE usuarioID = @id";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@username", item.Username);
                command.Parameters.AddWithValue("@password", item.Password);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------VALIDATE USUARIO------------------------
        // -----------------------------------------------------------------
        public Usuario Validate(string username, string password)
        {
            Usuario U = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioAlta AND usuarioName = @username AND usuarioPass = @password;";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    U = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["usuarioID"]),
                        Username = reader["usuarioName"].ToString(),
                        Password = reader["usuarioPass"].ToString(),
                        Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                        Alta = Convert.ToBoolean(reader["usuarioAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return U;
        }


        // -----------------------------------------------------------------
        // ---------------------------GET USUARIOS--------------------------
        // -----------------------------------------------------------------
        public List<Usuario> GetAllUsuarios()
        {
            List<Usuario> ListadoUsuarios = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario U = new Usuario()
                            {
                                Id = Convert.ToInt32(reader["usuarioID"]),
                                Username = reader["usuarioName"].ToString(),
                                Password = reader["usuarioPass"].ToString(),
                                Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                                Alta = Convert.ToBoolean(reader["usuarioAlta"])
                            };
                            ListadoUsuarios.Add(U);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
                connection.Dispose();
            }

            return ListadoUsuarios;
        }

        public Usuario GetUsuarioById(int id)
        {
            Usuario U = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioAlta AND usuarioID = @id";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    U = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["usuarioID"]),
                        Username = reader["usuarioName"].ToString(),
                        Password = reader["usuarioPass"].ToString(),
                        Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                        Alta = Convert.ToBoolean(reader["usuarioAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return U;
        }

        public Usuario GetUsuarioByName(string username)
        {
            Usuario U = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Usuarios WHERE usuarioName = @username";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@username", username);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    U = new Usuario()
                    {
                        Id = Convert.ToInt32(reader["usuarioID"]),
                        Username = reader["usuarioName"].ToString(),
                        Password = reader["usuarioPass"].ToString(),
                        Rol = (Rol)Enum.Parse(typeof(Rol), reader["usuarioRol"].ToString()),
                        Alta = Convert.ToBoolean(reader["usuarioAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return U;
        }
    }
}
