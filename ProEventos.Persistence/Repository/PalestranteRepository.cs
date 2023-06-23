using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Models;
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

        public async Task<Palestrante> GetPalestranteByUserIdAsync(int userId, bool includeEventos)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.User)
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
                .Where(x => x.UserId == userId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<PageList<Palestrante>> GetAllPalestrantesAsync(PageParams pageParams, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include(x => x.User)
                .Include(x => x.RedesSociais);

            if (includeEventos)
            {
                query = query
                    .Include(x => x.PalestrantesEventos)
                    .ThenInclude(x => x.Evento);
            }

            query = query
                .AsNoTracking()
                .Where(x => (x.MiniCurriculo.ToLower().Contains(pageParams.Term.ToLower()) ||
                             x.User.PrimeiroNome.ToLower().Contains(pageParams.Term.ToLower()) ||
                             x.User.UltimoNome.ToLower().Contains(pageParams.Term.ToLower())) &&
                            x.User.Funcao == Domain.Enum.Funcao.Palestrante)
                .OrderBy(x => x.Id);

            return await PageList<Palestrante>.CreateAsync(query, pageParams.PageNumber, pageParams.PageSize);
        }
    }
}
