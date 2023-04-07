using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class BaseRepository : IBaseRespository
    {
        protected readonly ProEventosContext _context;

        public BaseRepository(ProEventosContext context)
        {
            _context = context;
            //_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().AddAsync(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }
        public void DeleteRange<T>(T entityArray) where T : class
        {
            _context.Set<T>().RemoveRange(entityArray);
        }        
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
