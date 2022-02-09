namespace CadeteriaWeb.Entities
{
    public class Cliente
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private bool alta;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public int Id { get => id; set => id = value; }
        public bool Alta { get => alta; set => alta = value; }

        public Cliente()
        {
            Alta = true;
        }

        public Cliente(int id, string nombre, string direccion, string telefono, bool alta)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Alta = alta;
        }

        public Cliente(string nombre, string direccion, string telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Alta = true;
        }
    }
}