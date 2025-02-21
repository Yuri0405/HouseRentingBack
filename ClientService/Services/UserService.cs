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

        public async Task<ServiceResult<IEnumerable<AppUser>>> GetUserAsync(UserQueryParams param)
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

            var result = await _userRepository.GetByCondition(lambda,param.Quantity);
            
            int count  = result.Count();
            
            if (count == 0)
            {
                return ServiceResult<IEnumerable<AppUser>>.Fail("User not found");
            }
            
            return ServiceResult<IEnumerable<AppUser>>.Ok(
                result.Select(r => new AppUser { Id = r.Id, Name = r.Name, Email = r.Email}));
        }

        public async Task<ServiceResult> AddUserAsync(AppUser input)
        {
            var user = new AppUser { Name = input.Name,UserName = input.Name, Password = input.Password, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return ServiceResult.Ok($"{user.Name} updated successfully");
            }
            
            return MapIdentityErrors(result,"Failed to create user");
        }

        public async Task<ServiceResult> UpdateUserAsync(AppUser input)
        {
            var user = await _userManager.FindByIdAsync(input.Id);

            if (user == null)
            {
                return ServiceResult.Fail("User not found");
            }
            
            user.Name = input.Name;
            user.UserName = input.Name;
            user.Password = input.Password;
            user.Email = input.Email;
            
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return ServiceResult.Ok(user.Name + " updated successfully");
            }
            
            return MapIdentityErrors(result,"Failed to update user");
        }

        public async Task<ServiceResult> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return ServiceResult.Fail("User not found");
            }
            
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return MapIdentityErrors(result);
            }
            
            
            return ServiceResult.Ok("User deleted successfully");
        }
        
        private ServiceResult MapIdentityErrors(IdentityResult result, string failureMessage = "Operation failed.")
        {
            if (result.Succeeded)
            {
                return ServiceResult.Ok();
            }

            string errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));

            return ServiceResult.Fail($"{failureMessage} Errors: {errorMessages}");
        }
    }
}
