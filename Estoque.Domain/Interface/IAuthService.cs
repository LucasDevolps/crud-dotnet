using EstoqueApi.DTOs;

namespace EstoqueApi.Interface
{
    public interface IAuthService
    {
        Task<bool> AuthenticateUser(LoginDto loginDto);
        Task<bool> RegisterUser(RegisterDto registerDto);
    }
}
