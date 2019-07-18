using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class FilaAsientos
    {
        public int FilaAsientosID { get; set; }
        [Display(Name = "Cines")]
        public int CinesID { get; set; }
        [Range(0, 100, ErrorMessage = "La capacidad de asientos no debe superar mas de 100")]
        [Display(Name = "Contador Asientos")]
        public int Contador_Asientos { get; set; }
        public virtual Cines Cines { get; set; }
        public virtual ICollection<DescripcionAsiento> DescripcionAsientos { get; set; }
    }
}