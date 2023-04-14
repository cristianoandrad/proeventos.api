using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class EventoRepository : BaseRepository, IEventoRepository
    {

        public EventoRepository(ProEventosContext context) : base(context)
        {
        }        

        public async Task<Evento> GetAllEventoByIdAsync(int userId, int eventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Palestrante);
            }

            query = query
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.Id == eventoId &&
                            x.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(int userId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query                    
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Palestrante);
            }

            query = query
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(int userId, string tema, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Palestrante);
            }

            query = query
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Where(x => x.Tema.ToLower().Contains(tema.ToLower()) &&
                            x.UserId == userId);

            return await query.ToArrayAsync();
        }
    }
}
