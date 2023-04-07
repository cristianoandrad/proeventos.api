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

        public async Task<Evento> GetAllEventoByIdAsync(int eventoId, bool includePalestrantes = false)
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
                .Where(x => x.Id == eventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
                .Include(x => x.Lotes)
                .Include(x => x.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                    .AsNoTracking()
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Palestrante);
            }

            query = query.OrderBy(x => x.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
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
                .Where(x => x.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}
