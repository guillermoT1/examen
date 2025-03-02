﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProyectoCine.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("CineDB", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<ProyectoCine.Models.Cines> Cines { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.Consumidor> Consumidors { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.DescripcionAsiento> DescripcionAsientoes { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.EstadoAsientos> EstadoAsientos { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.FilaAsientos> FilaAsientos { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.Reservas> Reservas { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.Peliculas> Peliculas { get; set; }

        public System.Data.Entity.DbSet<ProyectoCine.Models.ShowPelicula> ShowPeliculas { get; set; }
    }
}