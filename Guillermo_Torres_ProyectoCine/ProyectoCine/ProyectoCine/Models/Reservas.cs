using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class Reservas
    {
        public int ReservasID { get; set; }
        [Display(Name = "Cliente")]
        public int ConsumidorID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha Reserva")]
        public DateTime FechaReserva { get; set; }
        [Range(0, 100, ErrorMessage = "La capacidad de asientos no debe superar mas de 100")]
        [Display(Name = "Contador Asientos")]
        public int NumeroAsientos { get; set; }

        public virtual Consumidor Consumidor { get; set; }
        public virtual ICollection<DescripcionAsiento> DescripcionAsientos { get; set; }
    }
}