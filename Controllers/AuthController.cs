using EstoqueApi.Data;
using EstoqueApi.DTOs;
using EstoqueApi.Interface;
using EstoqueApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstoqueApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;
        private readonly AppDbContext _dbContext;

        public AuthController(IAuthService authService, IJwtService jwtService, AppDbContext dbContext)
        {
            _authService = authService;
            _jwtService = jwtService;
            _dbContext = dbContext;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var isAuthenticated = await _authService.AuthenticateUser(loginDto);

            if (isAuthenticated)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                var token = _jwtService.GenerateToken(user);

                return Ok(new { token, uuid = user.Uuid, message = "Login successful" });
            }

            return Unauthorized(new { message = "Invalid email or password" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isRegistered = await _authService.RegisterUser(registerDto);

            if (isRegistered)
            {
                return Ok(new { message = "Registration successful" });
            }

            return BadRequest(new { message = "Email is already in use" });
        }
    }
}
