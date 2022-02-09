using System.ComponentModel.DataAnnotations;

namespace CadeteriaWeb.Models.ViewModels
{
    public class UsuarioCreateViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")] // forza que se escriba algo ahi
        [StringLength(100)] // longitud maxima / validacion
        [Display(Name = "Usuario")] // como se ve en el form
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La contraseña debe tener al menos 3 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [Compare("Password", ErrorMessage = "Ambas contraseñas deben ser iguales")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Reingrese la contraseña")]
        public string PasswordRetry { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }


    public class UsuarioLoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [StringLength(100)]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }


    public class UsuarioUpdateViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")] 
        [StringLength(100)] 
        [Display(Name = "Usuario")] 
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La contraseña debe tener al menos 3 caracteres")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [Compare("Password", ErrorMessage = "Ambas contraseñas deben ser iguales")]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name = "Reingrese la contraseña")]
        public string PasswordRetry { get; set; }


        [Display(Name = "Error")]
        public string Message { get; set; }
    }
}
