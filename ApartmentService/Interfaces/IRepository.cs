﻿using System.Linq.Expressions;

namespace ApartmentService.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetByCondition(Expression<Func<T,bool>> condition);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
