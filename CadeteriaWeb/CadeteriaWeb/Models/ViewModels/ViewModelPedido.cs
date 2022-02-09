using CadeteriaWeb.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CadeteriaWeb.Models.ViewModels
{
    public class PedidoCreateViewModel
    {
        public int IdCadete { get; set; }

        [Required(ErrorMessage = "Debe ingresar una observacion")] // forza que se escriba algo ahi
        [StringLength(255)] // longitud maxima / validacion
        [Display(Name = "Observación")] // como se ve en el form
        public string Observacion { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un nombre")]
        [StringLength(200)]
        [Display(Name = "Nombre del cliente")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "El cliente debe tener una direccion")]
        [StringLength(100)]
        [Display(Name = "Direccion del cliente")]
        public string DireccionCliente { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un numero de telefono")]
        [StringLength(20)]
        [Display(Name = "Nro de telefono del cliente")]
        public string TelefonoCliente { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }


    public class PedidoUpdateViewModel
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Estado { get; set; }

        [Required(ErrorMessage = "Debe ingresar una observacion")] // forza que se escriba algo ahi
        [StringLength(255)] // longitud maxima / validacion
        [Display(Name = "Observación")] // como se ve en el form
        public string Observacion { get; set; }


        [Required(ErrorMessage = "El cliente debe tener un nombre")]
        [StringLength(200)]
        [Display(Name = "Nombre del cliente")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "El cliente debe tener una direccion")]
        [StringLength(100)]
        [Display(Name = "Direccion del cliente")]
        public string DireccionCliente { get; set; }

        [Required(ErrorMessage = "El cliente debe tener un numero de telefono")]
        [StringLength(20)]
        [Display(Name = "Nro de telefono del cliente")]
        public string TelefonoCliente { get; set; }

        [Display(Name = "Cadete")]
        public int IdCadete { get; set; }

        public List<Cadete> Cadetes { get; set; }

        [Display(Name = "Error")]
        public string Message { get; set; }
    }
}
