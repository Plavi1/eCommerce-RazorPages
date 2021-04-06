using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripovi.Data.Data
{
    public static class ModelExtensionBuilder
    {
        public static void SeedSuperAdmin(this ModelBuilder modelBuilder)
        {
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";


            // Pravimo Admin Role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            // Pravimo Korisnika
            var Korisnik = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "Admin@admin.com",
                EmailConfirmed = true,
                UserName = "Admin@admin.com",
                LockoutEnabled = true,
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMIN@ADMIN.COM"
            };

            //Setuj Korisniku PW
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            Korisnik.PasswordHash = ph.HashPassword(Korisnik, "Sifra1");

            //Ubaci Korisnika
            modelBuilder.Entity<IdentityUser>().HasData(Korisnik);


            //Postavi Korisnika za Admina
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
        public static void MockStripovi(this ModelBuilder modelBuilder)
        {

            List<Strip> stripList = new List<Strip>(){
                new Strip(){IdStripa = 1, Naziv = "Mister NO",Naslov = "Cena izdaje", GodinaIzdanja = "2010", Jezik = "Srpski", Izdavac = "Veseli Cetvrtak", Cena = 230, Stanje="Novo", imgRoute = "MisterNO.jpg" },
                new Strip(){IdStripa = 2, Naziv = "Dilan Dog",Naslov = "Prica o Dilanu Dogu", GodinaIzdanja = "2006", Jezik = "Srpski", Izdavac = "Veseli Cetvrtak", Cena = 300, Stanje="Novo", imgRoute = "DilanDog.jpg" },
                new Strip(){IdStripa = 3, Naziv = "Alan Ford",Naslov = "Tako je nastala grupa", GodinaIzdanja = "1995", Jezik = "Srpski", Izdavac = "Classic", Cena = 250, Stanje="Novo", imgRoute = "AlanFord.jpg" },
                new Strip(){IdStripa = 4, Naziv = "Blek",Naslov ="Pobuna Trapera", GodinaIzdanja = "2000", Jezik = "Srpski", Izdavac = "Wizard", Cena = 300, Stanje="Polovno", imgRoute = "Blek.jpg" },
                new Strip(){IdStripa = 5, Naziv = "Zagor",Naslov="Gospodari", GodinaIzdanja = "2012", Jezik = "Srpski", Izdavac = "Veseli Cetvrtak", Cena = 100, Stanje="Polovno", imgRoute = "Zagor.jpg" },
            };

            modelBuilder.Entity<Strip>().HasData(stripList);
        }
        public static void SeedKorisnika(this ModelBuilder modelBuilder)
        {
            string KORISNIK1_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf1";


            // Pravimo Korisnika
            var Korisnik1 = new IdentityUser
            {
                Id = KORISNIK1_ID,
                Email = "korisnik1@korisnik.com",
                EmailConfirmed = true,
                UserName = "korisnik1@korisnik.com",
                LockoutEnabled = true,
                NormalizedEmail = "KORISNIK1@KORISNIK.COM",
                NormalizedUserName = "KORISNIK1@KORISNIK.COM"
            };

            //Setuj Korisniku PW
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            Korisnik1.PasswordHash = ph.HashPassword(Korisnik1, "Sifra1");
           

            //Ubaci Korisnika
            modelBuilder.Entity<IdentityUser>().HasData(Korisnik1);

        }
    }
}
