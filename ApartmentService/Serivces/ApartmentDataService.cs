using ApartmentService.Interfaces;
using ApartmentService.Models;
using System.Linq.Expressions;
using WebAPIGateway.Models;

namespace ApartmentService.Serivces
{
    public class ApartmentDataService
    {
        private readonly IRepository<Apartment> _repository;
        public ApartmentDataService(IRepository<Apartment> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Apartment>> GetApartments(ApartmentQueryParams queryParams)
        {
            var parameter = Expression.Parameter(typeof(Apartment), "r");

            Expression predicate = Expression.Constant(true);

            if (!string.IsNullOrEmpty(queryParams.City))
            {
                var cityCondition = Expression.Equal(
                    Expression.Property(parameter, nameof(Apartment.City)),
                    Expression.Constant(queryParams.City)
                    );
                predicate = Expression.AndAlso(predicate, cityCondition);
            }
            if (queryParams.Rooms.HasValue)
            {
                var roomsCondition = Expression.Equal(
                    Expression.Property(parameter, nameof(Apartment.NumberOfRooms)),
                    Expression.Constant(queryParams.Rooms.Value)
                    );
                predicate = Expression.AndAlso(predicate, roomsCondition);
            }

            var publishedCondition = Expression.NotEqual(
                Expression.Property(parameter, nameof(Apartment.IsRented)),
                Expression.Constant(true)
                );
            predicate = Expression.AndAlso(predicate, publishedCondition);

            var lambda = Expression.Lambda<Func<Apartment, bool>>(predicate, parameter);

            var result = await _repository.GetByCondition(lambda);

            if (result == null)
            {
                return new List<Apartment>();
            }

            return result;
        }

        public async Task<IEnumerable<Apartment>> GetApartmentPublishedByUser(Guid userId)
        {
            var parameter = Expression.Parameter(typeof(Apartment), "r");

            Expression predicate = Expression.Constant(true);

            var userIdCondition = Expression.Equal(
                Expression.Property(parameter, nameof(Apartment.OwnerId)),
                Expression.Constant(userId)
                );
            predicate = Expression.AndAlso(predicate, userIdCondition);

            var lambda = Expression.Lambda<Func<Apartment, bool>>(predicate, parameter);

            var result = await _repository.GetByCondition(lambda);

            if (result == null)
            {
                return new List<Apartment>();
            }

            return result;
        }
        public async Task AddApartmentsAsync(Apartment apartment)
        {
            await _repository.AddAsync(apartment);
        }

        public async Task UpdateApartmentsAsync(Apartment apartment)
        {
            await _repository.UpdateAsync(apartment);
        }

        public async Task DeleteApartmentAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
