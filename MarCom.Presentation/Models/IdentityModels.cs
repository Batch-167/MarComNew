using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;

namespace MarCom.Presentation.Models
{
    public class MUserLogin : IdentityUserLogin<int> { }
    public class MUserRole : IdentityUserRole<int> { }
    public class MUserClaim : IdentityUserClaim<int> { }

    public class MRole : IdentityRole<int, MUserRole>
    {
        public MRole() { }
        public MRole(string name) { Name = name; }

        public string Description { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        
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

        public int M_Employee_Id { get; set; }

        public bool Is_Delete { get; set; }

        [Required]
        [StringLength(50)]
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        [StringLength(50)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public int M_Role_Id { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, MRole, int, MUserLogin, MUserRole, MUserClaim >
    {
        public ApplicationDbContext()
            : base("IdentityConnection")
        {
        }

        public virtual DbSet<MUserRole> M_User_Role { get; set; }

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