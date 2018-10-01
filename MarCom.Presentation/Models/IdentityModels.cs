using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarCom.Presentation.Models
{
    public class MUserLogin : IdentityUserLogin<int> { }
    public class MUserRole : IdentityUserRole<int> { }
    public class MUserClaim : IdentityUserClaim<int> { }

    public class MRole : IdentityRole<int, MUserRole>
    {
        public MRole() { }
        public MRole(string name) { Name = name; }
    }

    public class CustomUserStore  : UserStore<ApplicationUser, MRole, int, MUserLogin, MUserRole, MUserClaim >
    {
        public CustomUserStore(ApplicationDbContext context)
            : base (context)
        {

        }
    }

    public class MRoleStore : RoleStore<MRole, int, MUserRole>
    {
        public MRoleStore(ApplicationDbContext context)
            : base(context)
        {

        }
    }

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<int, MUserLogin, MUserRole, MUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, int> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, MRole, int, MUserLogin, MUserRole, MUserClaim >
    {
        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("M_User");
            modelBuilder.Entity<MRole>().ToTable("M_Role");
            modelBuilder.Entity<MUserRole>().ToTable("M_User_Role");
            modelBuilder.Entity<MUserLogin>().ToTable("M_User_Login");
            modelBuilder.Entity<MUserClaim>().ToTable("M_User_Claim");
            modelBuilder.Entity<M_Menu>().ToTable("M_Menu");
            modelBuilder.Entity<M_Menu_Access>().ToTable("M_Menu_Access");
        }
    }
}