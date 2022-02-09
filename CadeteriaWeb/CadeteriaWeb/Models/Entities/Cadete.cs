namespace CadeteriaWeb.Entities
{
    public class Cadete
    {
        private int id;
        private string nombre;
        private string direccion;
        private string telefono;
        private double jornal;
        private bool alta;

        public int Id { get => id; set => id = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        public double Jornal { get => jornal; set => jornal = value; }
        public bool Alta { get => alta; set => alta = value; }

        public Cadete()
        {
            Jornal = 0;
            Alta = true;
        }

        public Cadete(string nombre, string direccion, string telefono)
        {
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Jornal = 0;
            Alta = true;
        }

        public Cadete(int id, string nombre, string direccion, string telefono, bool alta)
        {
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
            Telefono = telefono;
            Jornal = 0;
            Alta = alta;
        }
    }
}
