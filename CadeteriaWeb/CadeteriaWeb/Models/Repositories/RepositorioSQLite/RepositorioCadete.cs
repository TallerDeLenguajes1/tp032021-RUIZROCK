using CadeteriaWeb.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioCadete : IRepositorioCadete
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorioCadete(string connection, Logger log)
        {
            this.connectionString = connection;
            this.log = log;
        }

        // -----------------------------------------------------------------
        // -------------------------DELETE CADETE--------------------------
        // -----------------------------------------------------------------
        public void DeleteCadete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Cadetes SET cadeteAlta = 0 WHERE cadeteID = @id;";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------INSERT CADETE--------------------------
        // -----------------------------------------------------------------
        public void InsertCadete(Cadete item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Cadetes(cadeteNombre, cadeteDireccion, cadeteTelefono, cadeteAlta)" +
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
        // -------------------------UPDATE CADETE--------------------------
        // -----------------------------------------------------------------
        public void UpdateCadete(Cadete item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Cadetes" +
                    $" SET cadeteNombre = @nombre, cadeteDireccion = @direccion, cadeteTelefono = @telefono" +
                    $" WHERE cadeteID = @id";

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
        // ---------------------------GET CADETES--------------------------
        // -----------------------------------------------------------------
        public List<Cadete> GetAllCadetes()
        {
            List<Cadete> ListadoCadetes = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Cadetes WHERE cadeteAlta";

                using (SQLiteCommand command = new SQLiteCommand(SQLQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cadete C = new Cadete()
                            {
                                Id = Convert.ToInt32(reader["cadeteID"]),
                                Nombre = reader["cadeteNombre"].ToString(),
                                Direccion = reader["cadeteDireccion"].ToString(),
                                Telefono = reader["cadeteTelefono"].ToString(),
                                Alta = Convert.ToBoolean(reader["cadeteAlta"])
                            };
                            ListadoCadetes.Add(C);
                        }
                        reader.Close();
                    }
                }
                connection.Close();
                connection.Dispose();
            }

            return ListadoCadetes;
        }

        public Cadete GetCadeteById(int id)
        {
            Cadete C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Cadetes WHERE cadeteAlta AND cadeteID = @id";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    C = new Cadete()
                    {
                        Id = Convert.ToInt32(reader["cadeteID"]),
                        Nombre = reader["cadeteNombre"].ToString(),
                        Direccion = reader["cadeteDireccion"].ToString(),
                        Telefono = reader["cadeteTelefono"].ToString(),
                        Alta = Convert.ToBoolean(reader["cadeteAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return C;
        }

        public Cadete GetCadeteByName(string nombre)
        {
            Cadete C = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Cadetes WHERE cadeteAlta AND cadeteNombre = @nombre";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@nombre", nombre);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    C = new Cadete()
                    {
                        Id = Convert.ToInt32(reader["cadeteID"]),
                        Nombre = reader["cadeteNombre"].ToString(),
                        Direccion = reader["cadeteDireccion"].ToString(),
                        Telefono = reader["cadeteTelefono"].ToString(),
                        Alta = Convert.ToBoolean(reader["cadeteAlta"])
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
