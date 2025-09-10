using EstoqueApi.Data;
using EstoqueApi.DTOs;
using EstoqueApi.Interface;
using EstoqueApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AuthenticateUser(LoginDto loginDto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return false;
            }

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            return isPasswordValid;
        }
        public async Task<bool> RegisterUser(RegisterDto registerDto)
        {
            if (await _dbContext.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return false;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = new User
            {
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
