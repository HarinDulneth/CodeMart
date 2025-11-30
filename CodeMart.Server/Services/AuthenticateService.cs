using CodeMart.CodeMart.Server.Models;
using CodeMart.Server.Interfaces;

namespace CodeMart.Server.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthenticateService(IUserService userService, IJwtTokenService JwtTokenService)
        {
            _userService = userService;
            _jwtTokenService = JwtTokenService;
        }

        public async Task<string?> Login(string email, string password)
        {
            var user = await _userService.ValidateUserCredentialsAsync(email, password);

            if (user == null)
            {
                return null;
            }

            string token = _jwtTokenService.GenerateToken(user);
            return token;
        }
    }
}
