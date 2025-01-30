using ClientService.Models;
using ClientService.Services.Interfaces;

namespace ClientService.Services;

public class JwtAuthService
{
    private readonly IRepository<AppUser> _userRepository;
    private readonly JwtTokenHelpers _jwtTokenHelpers;

    public JwtAuthService(IRepository<AppUser> userRepository, JwtTokenHelpers jwtTokenHelpers)
    {
        _userRepository = userRepository;
        _jwtTokenHelpers = jwtTokenHelpers;
    }
    
    public async Task<string?> Authenticate(AppUser user)
    {
        var response = await _userRepository.GetByCondition(r => r.Name == user.Name && r.Password == user.Password);

        if (response == null)
        {
            return null;
        }

        var checkedUser = response.First();

        if (checkedUser != null)
        {
            return  await _jwtTokenHelpers.GenerateToken(checkedUser);
        }

        return null;
    }
}