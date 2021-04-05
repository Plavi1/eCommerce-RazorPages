using Microsoft.EntityFrameworkCore;
using Stripovi.Data.Data;
using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.KorpaRepository
{
    public class SQLKorpaRepository : IKorpaRepository
    {
        private readonly ApplicationDbContext context;

        public SQLKorpaRepository(ApplicationDbContext context)
        {
            this.context = context;

        }
        public async Task<Korpa> AddKorpa(Korpa korpa)
        {
            var result = await context.Korpa.AddAsync(korpa);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Korpa> DeleteSveUKorpi(string userId)
        {

            var result = context.Korpa.ToList().Where(e => e.UserId == userId);

            if (result != null)
            {
                foreach (var item in result)
                {
                    context.Korpa.Remove(item);
                    await context.SaveChangesAsync();
                }
            }
            return null;
        }

        public async Task<Korpa> DeleteStripUKorpi(int stripid)
        {
            var result = context.Korpa.FirstOrDefault(e => e.IdStripa == stripid);

            if (result != null)
            {
                context.Korpa.Remove(result);
                await context.SaveChangesAsync();
                return result;

            }
            return null;
        }

        public async Task<Korpa> GetKorpa(int korpaId)
        {
            return await context.Korpa
           .FirstOrDefaultAsync(e => e.Id == korpaId);
        }

        public async Task<IEnumerable<Korpa>> GetKorpe()
        {
            return await context.Korpa.ToListAsync();
        }

        public async Task<Korpa> UpdateKorpa(Korpa korpaPromena)
        {
            var result = await context.Korpa
                .FirstOrDefaultAsync(e => e.Id == korpaPromena.IdStripa);

            if (result != null)
            {
                result.IdStripa = korpaPromena.IdStripa;
                result.UserId = korpaPromena.UserId;

                await context.SaveChangesAsync();

                return result;
            }

            return null;
        }

        public int UserBrojStripovaUkorpi(string userId)
        {
            return context.Korpa.ToList().Where(e => e.UserId == userId).Count();
        }

        public async Task<IEnumerable<Korpa>> UserStripoviuKorpi(string userId)
        {
            var stripoviUKorpi = await context.Korpa.ToListAsync();

            return stripoviUKorpi.Where(e => e.UserId == userId);
        }
    }
}
