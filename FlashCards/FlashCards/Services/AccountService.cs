using FlashCards.DTOs;
using FlashCards.Model;
using FlashCards.Repository;
using FlashCards.Utilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FlashCards.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ConfigModel _configModel;
        public AccountService(
            IAccountRepository accountRepository,
            IOptions<ConfigModel> configModel
        )
        {
            _accountRepository = accountRepository;
            _configModel = configModel.Value;
        }
        public async Task<AuthResponse> Login(AuthRequest request)
        {
            try
            {
                var user = await _accountRepository.GetUserByName(request.UserName);

                if (user == null)
                {
                    throw new Exception("No such user exists!");
                }

                var result = await _accountRepository.MatchPassword(user, request.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("Password is wrong!");
                }

                AuthResponse response = new AuthResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    JWToken = await GenerateJwtToken(user)
                };


                return response;
            }
            catch
            {
                throw new Exception("Failed to login. Please try again.");
            }
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var signingKey = Convert.FromBase64String(_configModel.Jwt.SigningSecret);
            var expiryDuration = _configModel.Jwt.ExpiryDuration ?? 120;
            var validIssuer = _configModel.Jwt.ValidIssuer;
            var validAudience = _configModel.Jwt.ValidAudience;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),

            };

            var roles = await _accountRepository.GetUserRoles(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = validIssuer,
                Audience = validAudience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds
            };

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
