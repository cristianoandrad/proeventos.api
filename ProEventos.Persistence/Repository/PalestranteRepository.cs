using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class PalestranteRepository : BaseRepository, IPalestranteRepository
    {

        public PalestranteRepository(ProEventosContext context) : base(context)
        {
        }

        public async Task<Palestrante> GetAllPalestranteByIdAsync(int palestranteId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.Id == palestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Evento);
            }

            query = query
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.User.PrimeiroNome.ToLower().Contains(nome.ToLower()) ||
                            x.User.UltimoNome.ToLower().Contains(nome.ToLower())
                            );

            return await query.ToArrayAsync();
        }
               
    }
}
