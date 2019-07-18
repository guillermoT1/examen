using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class Cines
    {

        public int CinesID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese Un Nombre de Cine"), MaxLength(30)]
        [Display(Name = "Nombre Cine")]
    public string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese Un Administrador del Cine"), MaxLength(30)]
        [Display(Name = "Nombre Administrador")]
        public string Administrador { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese Una Direccion"), MaxLength(30)]
        [Display(Name = "Direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Ingrese un Número de Teléfono")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Numero de Telefono invalido")]
        [Display(Name = "Teléfono")]
        public int Telefono { get; set; }

        [Range(0, 100, ErrorMessage = "La capacidad de asientos no debe superar mas de 100")]
        [Display(Name = "Capacidad de Asientos")]
        public int Capcidad_Asientos { get; set; }
        [Display(Name = "Detalles")]
        public string OtrosDetalles { get; set; }

        public virtual ICollection<ShowPelicula> ShowPeliculas { get; set; }
        public virtual ICollection<FilaAsientos> FilaAsientos { get; set; }
    }
}