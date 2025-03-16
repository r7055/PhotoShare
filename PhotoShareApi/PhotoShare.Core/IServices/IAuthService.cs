using PhotoShare.Core.DTOs;
using PhotoShare.Core.Models;


namespace PhotoShare.Core.IServices
{
    public interface IAuthService
    {
        Task<UserDto> RegisterUser(UserDto userDto);
        Task<string> LoginUser(string email, string password);
        string GenerateJwtToken(UserDto user, string[] roles);
    }
}
