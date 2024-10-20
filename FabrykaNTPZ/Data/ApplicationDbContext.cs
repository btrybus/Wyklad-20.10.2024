using FabrykaNTPZ.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FabrykaNTPZ.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<OperatorMaszyna>().HasKey(om => new { om.MaszynaId, om.OperatorId });

            builder.Entity<OperatorMaszyna>()
                .HasOne(om => om.Maszyna)
                .WithMany(m => m.OperatorMaszyna)
                .HasForeignKey(om => om.MaszynaId);

            builder.Entity<OperatorMaszyna>()
                .HasOne(om => om.Operator)
                .WithMany(o => o.OperatorMaszyna)
                .HasForeignKey(om => om.OperatorId);
        }

        public DbSet<Hala> Hale { get; set; }
        public DbSet<Maszyna> Maszyny { get; set; }
        public DbSet<Operator> Operatorzy { get; set; }
        public DbSet<OperatorMaszyna> Operator_Maszyna { get; set; }

    }
}
