using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Data;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.StripRepository
{
    public class SQLStripRepository : IStripRepository
    {
        private readonly ApplicationDbContext context;

        public SQLStripRepository(ApplicationDbContext context)
        {
            this.context = context;
        }


        public async Task<Strip> AddStrip(Strip strip)
        {
            var result = await context.Strip.AddAsync(strip);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Strip> GetStrip(int stripId)
        {
            return await context.Strip
                 .FirstOrDefaultAsync(e => e.IdStripa == stripId);
        }

        public async Task<IEnumerable<Strip>> GetStripove()
        {
            return await context.Strip.ToListAsync();
        }

        public async Task<Strip> UpdateStrip(Strip stripPromena)
        {
            var result = await context.Strip
                .FirstOrDefaultAsync(e => e.IdStripa == stripPromena.IdStripa);

            if (result != null)
            {
                result.Naziv = stripPromena.Naziv;
                result.Naslov = stripPromena.Naslov;
                result.Izdavac = stripPromena.Izdavac;
                result.Stanje = stripPromena.Stanje;
                result.Jezik = stripPromena.Jezik;
                result.GodinaIzdanja = stripPromena.GodinaIzdanja;
                result.Cena = stripPromena.Cena;

                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public async Task<Strip> DeleteStrip(int stripId)
        {
            var result = await context.Strip
                .FirstOrDefaultAsync(e => e.IdStripa == stripId);
            if (result != null)
            {
                context.Strip.Remove(result);
                await context.SaveChangesAsync();
                return result;
            }
            return null;
        }


        public async Task<IEnumerable<Strip>> UserStripoviVanKorpe(string userId)
        {
            var StripoviUKorpi = context.Korpa.Where(e => e.UserId == userId)
                                              .Select(e => new { e.IdStripa });

            IEnumerable<Strip> sviStripovi = await context.Strip.ToListAsync();

            foreach (var item in StripoviUKorpi)
            {
                sviStripovi = sviStripovi.Where(e => e.IdStripa != item.IdStripa);
            }

            return sviStripovi;
        }

        public async Task<IEnumerable<Strip>> Search(string naslov, string userId)
        {
            IQueryable<Strip> query = context.Strip;

            if (!string.IsNullOrEmpty(naslov))
            {
                query = query.Where(e => e.Naziv.Contains(naslov) || e.Naslov.Contains(naslov));
            }

            var StripoviUKorpi = context.Korpa.Where(e => e.UserId == userId)
                                             .Select(e => new { e.IdStripa });

            foreach (var item in StripoviUKorpi)
            {
                query = query.Where(e => e.IdStripa != item.IdStripa);
            }

            return await query.ToListAsync();
        }
    }
}
