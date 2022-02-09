namespace CadeteriaWeb.Entities
{
    public enum EstadoPedido { Vacio, Procesando, En_Camino, Entregado, Cancelado };

    public class Pedido
    {
        private int id;
        private string observacion;
        private Cliente cliente;
        private EstadoPedido estado;
        private Cadete cadete;
        private bool alta;

        public int Id { get => id; set => id = value; }
        public string Observacion { get => observacion; set => observacion = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public EstadoPedido Estado { get => estado; set => estado = value; }
        public Cadete Cadete { get => cadete; set => cadete = value; }
        public bool Alta { get => alta; set => alta = value; }

        public Pedido()
        {
            Estado = EstadoPedido.Vacio;
            Cadete = null;
            Cliente = null;
            Alta = true;
        }

        public Pedido(string observacion, string nombreCliente, string direccionCliente, string telefonoCliente)
        {
            Observacion = observacion;
            Estado = EstadoPedido.Procesando;
            Cadete = null;
            Cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente);
            Alta = true;
        }

        public Pedido(int id, string observacion, Cliente cliente, EstadoPedido estado, Cadete cadete, bool alta)
        {
            Id = id;
            Observacion = observacion;
            Cliente = cliente;
            Estado = estado;
            Cadete = cadete;
            Alta = alta;
        }

        // -------------------------------------------------------------------------------------------
        public void AsignarCadete(Cadete C)
        {
            Cadete = C;
            Estado = EstadoPedido.En_Camino;
        }

        public void Entregar()
        {
            //Cadete.Jornal += 100;
            Estado = EstadoPedido.Entregado;
        }

        public void Cancelar()
        {
            Estado = EstadoPedido.Cancelado;
        }
    }
}