using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class EstadoAsientos
    {
        public int EstadoAsientosID { get; set; }
        [Display(Name = "Detalles Asientos")]
        public string DestallesAsiento { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese el estado del Asiento"), MaxLength(30)]
        [Display(Name = "Disponibilidad Asiento")]
        public string Disponible { get; set; }
        [Display(Name = "Asiento No disponible")]
        public string NoDisponible { get; set; }
        public virtual ICollection<DescripcionAsiento> DescripcionAsientos { get; set; }

    }
}