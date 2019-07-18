using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class Peliculas
    {
        public int PeliculasID { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese Un Nombre de Pelicula"), MaxLength(30)]
        [Display(Name = "Nombre Pelicula")]
        public string Nombre { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese la Categoría"), MaxLength(30)]
        [Display(Name = "Categoría")]
        public string Descripcion { get; set; }

        public virtual ICollection<ShowPelicula> ShowPeliculas { get; set; }
    }
}