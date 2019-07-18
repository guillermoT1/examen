using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class ShowPelicula
    {
        public int ShowPeliculaID { get; set; }

        public int CinesID { get; set; }
        [Display(Name = "Peliculas")]
        public int PeliculasID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Estreno")]
        public DateTime ShowInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Fin de Publicacion")]
        public DateTime ShowFin { get; set; }

        public virtual Cines Cines { get; set; }
        public virtual Peliculas Peliculas { get; set; }

    }
    
}