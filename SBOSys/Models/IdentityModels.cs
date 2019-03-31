using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SBOSys.Models
{
    public class ApplicationUser : IdentityUser
    {

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
        {
            public ApplicationDbContext()
                : base("PegasusEntities2", throwIfV1Schema: false)
            {
            }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.Entity<ApplicationUser>().ToTable("Users");
                modelBuilder.Entity<IdentityRole>().ToTable("Roles");
                modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
                modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
                modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");

            }


            public static ApplicationDbContext Create()
            {
                return new ApplicationDbContext();
            }
        }

    }

}