using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Identity;
using ProEventos.Persistence.Context;
using ProEventos.Persistence.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(ProEventosContext context) : base(context)
        {
            
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower());
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
