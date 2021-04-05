using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Data;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.PorudzbinaRepository
{
    public class SQLPorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly ApplicationDbContext context;

        public SQLPorudzbinaRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Porudzbina> AddPorudzbinu(Porudzbina porudzbina, List<int> IdstripovaUPorudzbini)
        {
            var result = await context.Porudzbina.AddAsync(porudzbina);
            await context.SaveChangesAsync();

            StripInPorudzbina novStripUPorudzbini = new StripInPorudzbina() { IdPorudzbine = porudzbina.IdPorudzbine };
            foreach (var item in IdstripovaUPorudzbini)
            {
                novStripUPorudzbini.IdStripa = item;
                await context.StripInPorudzbina.AddAsync(novStripUPorudzbini);
                await context.SaveChangesAsync();
            }

            return result.Entity;
        }

        public async Task<Porudzbina> DeletePorudzbinu(int porudzbinaId)
        {
            var porudzbina = await context.Porudzbina
                .FirstOrDefaultAsync(e => e.IdPorudzbine == porudzbinaId);

            if (porudzbina != null)
            {
                context.Porudzbina.Remove(porudzbina);
                await context.SaveChangesAsync();

                return porudzbina;
            }
            return null;
        }

        public async Task<IEnumerable<Porudzbina>> GetPorudzbine()
        {
            return await context.Porudzbina.ToListAsync();
        }

        public async Task<Porudzbina> GetPorudzbinu(int porudzbinaId)
        {
            return await context.Porudzbina.FirstOrDefaultAsync(e => e.IdPorudzbine == porudzbinaId);
        }

        public async Task<IEnumerable<Strip>> GetSveStripoveuPorudzbini(int porudzbinaId)                                                  //Proveri
        {
            var stripoviUPorudzbini = context.StripInPorudzbina.ToList().Where(e => e.IdPorudzbine == porudzbinaId).Select(e => e.IdStripa);
            if (stripoviUPorudzbini != null)
            {
                List<Strip> stripovi = new List<Strip>();
                foreach (var strip in stripoviUPorudzbini)
                {
                    stripovi.Add(await context.Strip.FirstOrDefaultAsync(e => e.IdStripa == strip));
                }

                return stripovi;
            }
            return null;
        }

        public int UserBrojPorudzbina(string userId)
        {
            return context.Porudzbina.ToList().Where(e => e.UserId == userId).Count();
        }
    }
}
