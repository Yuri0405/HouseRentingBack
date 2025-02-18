using ClientService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ClientService.Services.Interfaces;

namespace ClientService.Services
{
    public class UserRepository : IRepository<AppUser>
    {
        private ApartmentClientDbContext _context;
        public UserRepository(ApartmentClientDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUser>?> GetByCondition(Expression<Func<AppUser, bool>> condition,int? limit)
        {
            var  query = _context.AppUsers.Where(condition); 
            if (limit.HasValue)
            {
                query = query.Take(limit.Value);
            }
            
            return await query.ToListAsync();
        }

        public async Task AddAsync(AppUser user)
        {
            await _context.AppUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await _context.AppUsers.FindAsync(id);
            _context.AppUsers.Remove(record);
        }
    }
}
