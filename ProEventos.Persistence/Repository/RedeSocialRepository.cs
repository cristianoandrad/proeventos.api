using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class RedeSocialRepository : BaseRepository, IRedeSocialRepository
    {
        public RedeSocialRepository(ProEventosContext context) : base(context)
        {
            _context = context;
        }

        public ProEventosContext _context { get; }

        public async Task<RedeSocial[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.AsNoTracking().Where(x => x.PalestranteId == palestranteId).ToArrayAsync();
        }

        public async Task<RedeSocial[]> GetAllByEventoIdAsync(int eventoId)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.AsNoTracking().Where(x => x.EventoId == eventoId).ToArrayAsync();
        }

        public async Task<RedeSocial> GetRedeSocialEventoByIdsAsync(int eventoId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.EventoId == eventoId && x.Id == id);
        }

        public async Task<RedeSocial> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int id)
        {
            IQueryable<RedeSocial> query = _context.RedesSociais;

            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.PalestranteId == palestranteId && x.Id == id);
        }
    }
}
