using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Models;

namespace Stripovi.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Strip> Strip { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Porudzbina> Porudzbina { get; set; }
        public DbSet<StripInPorudzbina> StripInPorudzbina { get; set; }
        public DbSet<Kontakt> Kontakt { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.SeedSuperAdmin();
            builder.MockStripovi();
            builder.SeedKorisnika();
            builder.Entity<StripInPorudzbina>().HasKey(e => new { e.IdPorudzbine, e.IdStripa });

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
