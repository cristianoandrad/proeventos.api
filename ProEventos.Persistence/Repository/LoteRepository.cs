using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Models;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class LoteRepository : BaseRepository, ILoteRepository
    {

        public LoteRepository(ProEventosContext context) : base(context)
        {
        }

        public async Task<Lote> GetLoteByIdsAsync(int eventoId, int id)
        {
            return await _context.Lotes.AsNoTracking().FirstOrDefaultAsync(x => x.EventoId == eventoId && x.Id == id); 
        }

        public async Task<Lote[]> GetLotesByEventoIdAsync(int eventoId)
        {
            return await _context.Lotes.AsNoTracking().Where(x => x.EventoId == eventoId).ToArrayAsync();
        }
    }
}
