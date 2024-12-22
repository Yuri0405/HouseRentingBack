using ApartmentService.Interfaces;
using ClientService.Models;
using System.Linq.Expressions;

namespace ClientService.Services
{
    public class UserService
    {
        private readonly IRepository<AspNetUser> _userRepository;

        public UserService(IRepository<AspNetUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetUserAsync(UserQueryParams param)
        {
            var parameter = Expression.Parameter(typeof(AspNetUser), "r");

            Expression predicate = Expression.Constant(true);

            Guid id;

            if (Guid.TryParse(param.Id, out id))
            {
                var idCondition = Expression.Equal(
                    Expression.Property(parameter, nameof(AspNetUser.Id)),
                    Expression.Constant(param.Id));

                predicate = Expression.AndAlso(predicate, idCondition);
            }

            var lambda = Expression.Lambda<Func<AspNetUser, bool>>(predicate, parameter);

            var result = await _userRepository.GetByCondition(lambda);

            if (result == null)
            {
                return new List<User>();
            }

            return result.Select(r => new User { Id = r.Id, Name = r.Name, Email = r.Email});
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddAsync(new AspNetUser { Name = user.Name,Password = user.Password, Email = user.Email,});
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(new AspNetUser {Id = user.Id, Name = user.Name, Password = user.Password, Email = user.Email, });
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
