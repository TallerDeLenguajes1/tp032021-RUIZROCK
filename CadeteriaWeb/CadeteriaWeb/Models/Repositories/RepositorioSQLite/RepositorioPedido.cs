using CadeteriaWeb.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace CadeteriaWeb.Repositories
{
    public class RepositorioPedido : IRepositorioPedido
    {
        private readonly string connectionString;
        private readonly Logger log;

        public RepositorioPedido(string connection, Logger log)
        {
            this.connectionString = connection;
            this.log = log;
        }

        // -----------------------------------------------------------------
        // -------------------------DELETE PEDIDO--------------------------
        // -----------------------------------------------------------------
        public void DeletePedido(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"UPDATE Pedidos SET pedidoAlta = 0 WHERE pedidoID = @id;";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------INSERT PEDIDO--------------------------
        // -----------------------------------------------------------------
        public void InsertPedido(Pedido item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"INSERT INTO Pedidos(pedidoObs, pedidoEstado, clienteID, pedidoAlta)" +
                    $" VALUES(@pedidoObs, @pedidoEstado, @clienteID, 1);";

                if (item.Cadete != null)
                {
                    SQLQuery = $"INSERT INTO Pedidos(pedidoObs, pedidoEstado, clienteID, cadeteID, pedidoAlta)" +
                    $" VALUES(@pedidoObs, @pedidoEstado, @clienteID, @cadeteID, 1);";
                }

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@pedidoObs", item.Observacion);
                command.Parameters.AddWithValue("@pedidoEstado", item.Estado.ToString());
                command.Parameters.AddWithValue("@clienteID", item.Cliente.Id);
                if (item.Cadete != null) command.Parameters.AddWithValue("@cadeteID", item.Cadete.Id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------UPDATE PEDIDO--------------------------
        // -----------------------------------------------------------------
        public void UpdatePedido(Pedido item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string SQLQuery = $"UPDATE Pedidos" +
                    $" SET pedidoObs = @pedidoObs, pedidoEstado = @pedidoEstado" +
                    $" WHERE pedidoID = @id";

                if (item.Cadete != null)
                {
                    SQLQuery = $"UPDATE Pedidos" +
                    $" SET pedidoObs = @pedidoObs, pedidoEstado = @pedidoEstado, cadeteID = @cadeteID" +
                    $" WHERE pedidoID = @id";
                }


                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", item.Id);
                command.Parameters.AddWithValue("@pedidoObs", item.Observacion);
                command.Parameters.AddWithValue("@pedidoEstado", item.Estado.ToString());
                if (item.Cadete != null) command.Parameters.AddWithValue("@cadeteID", item.Cadete.Id);

                command.ExecuteNonQuery();
                connection.Close();
                connection.Dispose();
            }
        }


        // -----------------------------------------------------------------
        // -------------------------GET PEDIDOS--------------------------
        // -----------------------------------------------------------------
        public List<Pedido> GetAllPedidos(int cadeteID = 0)
        {
            List<Pedido> ListadoPedidos = new();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Pedidos WHERE pedidoAlta";
                if (cadeteID != 0) SQLQuery += $" AND cadeteID = @cadeteID";

                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                if (cadeteID != 0) command.Parameters.AddWithValue("@cadeteID", cadeteID);

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var aux = reader["cadeteID"].ToString();
                    Cadete C = null;
                    if (aux != "")
                        C = new RepositorioCadete(connectionString, log).GetCadeteById(Convert.ToInt32(aux));
                    Cliente Ci = new RepositorioCliente(connectionString, log).GetClienteById(Convert.ToInt32(reader["clienteID"]));

                    Pedido P = new Pedido()
                    {
                        Id = Convert.ToInt32(reader["pedidoID"]),
                        Observacion = reader["pedidoObs"].ToString(),
                        Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), reader["pedidoEstado"].ToString()),
                        Cliente = Ci,
                        Cadete = C,
                        Alta = Convert.ToBoolean(reader["pedidoAlta"])
                    };
                    ListadoPedidos.Add(P);
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return ListadoPedidos;
        }

        public Pedido GetPedidoById(int id)
        {
            Pedido P = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string SQLQuery = $"SELECT * FROM Pedidos WHERE pedidoAlta AND pedidoID = @id";
                SQLiteCommand command = new SQLiteCommand(SQLQuery, connection);
                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                reader.Read();
                if (reader.HasRows)
                {
                    var aux = reader["cadeteID"].ToString();
                    Cadete C = null;
                    if (aux != "")
                        C = new RepositorioCadete(connectionString, log).GetCadeteById(Convert.ToInt32(aux));
                    Cliente Ci = new RepositorioCliente(connectionString, log).GetClienteById(Convert.ToInt32(reader["clienteID"]));

                    P = new Pedido()
                    {
                        Id = Convert.ToInt32(reader["pedidoID"]),
                        Observacion = reader["pedidoObs"].ToString(),
                        Estado = (EstadoPedido)Enum.Parse(typeof(EstadoPedido), reader["pedidoEstado"].ToString()),
                        Cliente = Ci,
                        Cadete = C,
                        Alta = Convert.ToBoolean(reader["pedidoAlta"])
                    };
                }
                reader.Close();
                connection.Close();
                connection.Dispose();
            }

            return P;
        }


    }
}
