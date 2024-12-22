using ApartmentService.Interfaces;
using ApartmentService.Models;
using ApartmentService.Models.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApartmentService.Serivces
{
    public class ApartmentRepository: IRepository<Apartment>
    {
        private readonly ApartmentDbContext _db;
        public ApartmentRepository(ApartmentDbContext db) 
        { 
            _db = db;
        }

        public async Task<IEnumerable<Apartment>> GetByCondition(Expression<Func<Apartment, bool>> condition)
        {
            var query = _db.Apartments.AsQueryable();
            query = query.Where(condition);
            var result = await query.ToListAsync();

            return result;
        }
        public async Task AddAsync(Apartment user)
        {
            await _db.Apartments.AddAsync(user);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Apartment user)
        {
            _db.Apartments.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await _db.Apartments.FindAsync(id);

            _db.Apartments.Remove(record);
            await _db.SaveChangesAsync();
        }
    }
}
