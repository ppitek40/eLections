using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace eLections.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string,IdentityUserLogin,IdentityUserRole,IdentityUserClaim>
    {
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Election> Elections { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}