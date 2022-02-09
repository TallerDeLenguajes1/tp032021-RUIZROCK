namespace CadeteriaWeb.Entities
{
    public enum Rol { ADMIN, USER, NOTLOGUED };

    public class Usuario
    {
        private int id;
        private string username;
        private string password;
        private Rol rol;
        private bool alta;

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public bool Alta { get => alta; set => alta = value; }
        public Rol Rol { get => rol; set => rol = value; }

        public Usuario()
        {
            Alta = true;
        }

        public Usuario(int id, string username, string password, Rol rol, bool alta)
        {
            Id = id;
            Username = username;
            Password = password;
            Rol = rol;
            Alta = alta;
        }

    }
}
