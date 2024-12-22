using ApartmentService.Interfaces;
using ClientService.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ClientService.Services
{
    public class UserRepository : IRepository<AspNetUser>
    {
        private ApartmentClientDbContext _context;
        public UserRepository(ApartmentClientDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AspNetUser>?> GetByCondition(Expression<Func<AspNetUser, bool>> condition)
        {
            var result = await _context.AspNetUsers.Where(condition).ToListAsync();

            return result;
        }

        public async Task AddAsync(AspNetUser user)
        {
            await _context.AspNetUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AspNetUser user)
        {
            _context.AspNetUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await _context.AspNetUsers.FindAsync(id);
            _context.AspNetUsers.Remove(record);
        }
    }
}
