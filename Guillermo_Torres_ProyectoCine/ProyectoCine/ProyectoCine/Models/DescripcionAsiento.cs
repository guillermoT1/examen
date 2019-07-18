using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class DescripcionAsiento
    {
        public int DescripcionAsientoID { get; set; }

        [Display(Name = "Fila Asientos")]
        public int FilaAsientosID { get; set; }
        [Display(Name = "Reservas")]
        public int ReservasID { get; set; }
        [Display(Name = "Estado Asientos")]
        public int EstadoAsientosID { get; set; }

        public virtual Reservas Reservas { get; set; }
        public virtual EstadoAsientos Estado { get; set; }
        public virtual FilaAsientos FilaAsientos { get; set; }

    }
}