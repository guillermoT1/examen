using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoCine.Models
{
    public class Consumidor
    {
        public int ConsumidorID { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Porfavor Ingrese Un Nombre"), MaxLength(30)]
        [Display(Name = "Nombre Cliente")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese un Número de Teléfono")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Numero de Telefono invalido")]
        [Display(Name = "Teléfono")]
        public int Telefono { get; set; }
        [Display(Name = "Detalles Tarjeta")]
        public string DetallesTarjeta { get; set; }
        [Display(Name = "Detalles Cliente")]
        public string DetallesConsumidor { get; set; }
        public virtual ICollection<Reservas> Reservas { get; set; }
    }
}