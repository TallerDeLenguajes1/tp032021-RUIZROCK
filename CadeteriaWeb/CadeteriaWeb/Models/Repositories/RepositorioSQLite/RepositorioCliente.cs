using CadeteriaWeb.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioCliente : IRepositorioCliente
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorioCliente(string connection, Logger log)
        {
            this.connectionString = connection;
            this.log = log;
        }

        // -----------------------------------------------------------------
        // -------------------------DELETE CLIENTE--------------------------
        // -----------------------------------------------------------------
        public void DeleteCliente(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Clientes SET clienteAlta = 0 WHERE clienteID = @id;";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------INSERT CLIENTE--------------------------
        // -----------------------------------------------------------------
        public void InsertCliente(Cliente item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Clientes(clienteNombre, clienteDireccion, clienteTelefono, clienteAlta)" +
                    $" VALUES(@nombre, @direccion, @telefono, 1);";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@nombre", item.Nombre);
                command.Parameters.AddWithValue("@direccion", item.Direccion);
                command.Parameters.AddWithValue("@telefono", item.Telefono);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------UPDATE CLIENTE--------------------------
        // -----------------------------------------------------------------
        public void UpdateCliente(Cliente item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Clientes" +
                    $" SET clienteNombre = @nombre, clienteDireccion = @direccion, clienteTelefono = @telefono" +
                    $" WHERE clienteID = @id";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@nombre", item.Nombre);
                command.Parameters.AddWithValue("@direccion", item.Direccion);
                command.Parameters.AddWithValue("@telefono", item.Telefono);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // ---------------------------GET CLIENTES--------------------------
        // -----------------------------------------------------------------
        public List<Cliente> GetAllClientes()
        {
            List<Cliente> ListadoClientes = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Clientes WHERE clienteAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cliente C = new Cliente()
                            {
                                Id = Convert.ToInt32(reader["clienteID"]),
                                Nombre = reader["clienteNombre"].ToString(),
                                Direccion = reader["clienteDireccion"].ToString(),
                                Telefono = reader["clienteTelefono"].ToString(),
                                Alta = Convert.ToBoolean(reader["clienteAlta"])
                            };
                            ListadoClientes.Add(C);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
                connection.Dispose();
            }

            return ListadoClientes;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Clientes WHERE clienteAlta AND clienteID = @id";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    C = new Cliente()
                    {
                        Id = Convert.ToInt32(reader["clienteID"]),
                        Nombre = reader["clienteNombre"].ToString(),
                        Direccion = reader["clienteDireccion"].ToString(),
                        Telefono = reader["clienteTelefono"].ToString(),
                        Alta = Convert.ToBoolean(reader["clienteAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return C;
        }

        public Cliente GetClienteByInfo(string nombre, string direccion, string telefono)
        {
            Cliente C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Clientes WHERE clienteAlta AND clienteNombre = @nombre AND clienteDireccion = @direccion AND clienteTelefono = @telefono";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@nombre", nombre);
                command.Parameters.AddWithValue("@direccion", direccion);
                command.Parameters.AddWithValue("@telefono", telefono);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    C = new Cliente()
                    {
                        Id = Convert.ToInt32(reader["clienteID"]),
                        Nombre = reader["clienteNombre"].ToString(),
                        Direccion = reader["clienteDireccion"].ToString(),
                        Telefono = reader["clienteTelefono"].ToString(),
                        Alta = Convert.ToBoolean(reader["clienteAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return C;
        }
    }
}
