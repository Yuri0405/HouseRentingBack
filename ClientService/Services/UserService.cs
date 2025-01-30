using ClientService.Models;
using System.Linq.Expressions;
using ClientService.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ClientService.Services
{
    public class UserService
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IRepository<AppUser> userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<AppUser>> GetUserAsync(UserQueryParams param)
        {
            var parameter = Expression.Parameter(typeof(AppUser), "r");

            Expression predicate = Expression.Constant(true);

            Guid id;

            if (Guid.TryParse(param.Id, out id))
            {
                var idCondition = Expression.Equal(
                    Expression.Property(parameter, nameof(AppUser.Id)),
                    Expression.Constant(param.Id));

                predicate = Expression.AndAlso(predicate, idCondition);
            }

            var lambda = Expression.Lambda<Func<AppUser, bool>>(predicate, parameter);

            var result = await _userRepository.GetByCondition(lambda);

            if (result == null)
            {
                return new List<AppUser>();
            }

            return result.Select(r => new AppUser { Id = r.Id, Name = r.Name, Email = r.Email});
        }

        public async Task AddUserAsync(AppUser input)
        {
            var user = new AppUser { Name = input.Name,UserName = input.Name, Password = input.Password, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            await _userRepository.UpdateAsync(new AppUser {Id = user.Id, Name = user.Name, Password = user.Password, Email = user.Email, });
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
