using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Models;
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

        public async Task<PageList<Evento>> GetAllEventosAsync(int userId, PageParams pageParams, bool includePalestrantes = false)
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
                .Where(x => (x.Tema.ToLower().Contains(pageParams.Term.ToLower()) ||
                             x.Local.ToLower().Contains(pageParams.Term.ToLower())) &&
                //.Where(x => x.Tema.ToLower().Contains(pageParams.Term != null ? pageParams.Term.ToLower() : x.Tema.ToLower()) &&
                            x.UserId == userId);

            return await PageList<Evento>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }        
    }
}
