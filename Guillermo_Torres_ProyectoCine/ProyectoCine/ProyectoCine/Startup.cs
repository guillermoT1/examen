using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProyectoCine.Models;

[assembly: OwinStartupAttribute(typeof(ProyectoCine.Startup))]
namespace ProyectoCine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            crearRolesUsuarios();
        }

        private void crearRolesUsuarios()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.UserName = "admin@udla.com";
                user.Email = "admin@udla.com";
                string userPWD = "Udla2017*";
                var chkUser = userManager.Create(user, userPWD);

                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
