using Stripovi.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stripovi.Data.Repositorys.KorpaRepository
{
    public interface IKorpaRepository
    {
        Task<IEnumerable<Korpa>> GetKorpe();
        Task<IEnumerable<Korpa>> UserStripoviuKorpi(string userId);
        Task<Korpa> GetKorpa(int korpaId);
        Task<Korpa> AddKorpa(Korpa korpa);
        Task<Korpa> UpdateKorpa(Korpa korpaPromena);
        Task<Korpa> DeleteStripUKorpi(int stripid);
        Task<Korpa> DeleteSveUKorpi(string userId);
        int UserBrojStripovaUkorpi(string userId);
    }
}
