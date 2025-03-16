using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using PhotoShare.Core.DTOs;
using PhotoShare.Core.IRepositories;
using PhotoShare.Core.IServices;
using PhotoShare.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PhotoShare.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public AuthService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<string> LoginUser(string email, string password)
        {
            var user = await _repositoryManager.User.GetByUserEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            // Verify the password (assuming you have a method to verify hashed passwords)
            if (!VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            var userDto = _mapper.Map<UserDto>(user);   
            // Generate JWT token (assuming you have a method to generate tokens)
            var token = GenerateJwtToken(userDto, user.Roles.Select(r => r.Description).ToArray());

            // Store the token in a secure place (e.g., HttpOnly cookie, local storage, etc.)
            // This part depends on your application's requirements

            return token;
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implement your password verification logic here
            // For example, using BCrypt:
            // return BCrypt.Net.BCrypt.Verify(password, storedHash);

            // Placeholder implementation
            return password == storedHash;
        }

        public async Task<UserDto> RegisterUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var res = await _repositoryManager.User.AddAsync(user);
            await _repositoryManager.SaveAsync();
            return _mapper.Map<UserDto>(res);
        }

        public string GenerateJwtToken(UserDto user, string[] roles)
        {
            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // ה-ID של המשתמש
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: null, // ניתן להוסיף אם יש צורך
                audience: null, // ניתן להוסיף אם יש צורך
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
