using IMSApi.Common;
using IMSApi.Common.Models;
using IMSApi.Common.Utils;
using IMSApi.Configuration;
using IMSApi.Services.Contracts;
using IMSApi.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IMSApi.Services.Modules
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly List<User> users = new List<User> {
            new User{ FirstName="test",LastName="test", Email="test@test.com", UserRole = "User", UserStatus=0, Password="test"}};
        private readonly JWTSettings _jWtSettings;

        public AuthenticationManager(IOptions<JWTSettings> jwtSettings)
        {
            _jWtSettings = jwtSettings.Value;
        }
        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            var user = users.Find(u => u.Email == request.UserName &&
                             u.Password == Helpers.GetSHA256Hash(request.Password));

            if (user == null)
            {
                return new AuthenticateResponse();
            }

            var token = GenerateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public async Task<Result> RegisterUser(RegisterUserRequest request)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedDate = DateTime.UtcNow,
                Password = Helpers.GetSHA256Hash(request.Password),
                UserRole = UserRole.User.ToString(),
                UserStatus = 0,
            };

            return new Result { };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jWtSettings.SecretKey);
            var claims = new ClaimsIdentity(new[] { 
                 new Claim("Id",user.UserId.ToString()),
                 new Claim(ClaimTypes.Email,user.Email),
                 new Claim(ClaimTypes.Role,user.UserRole)
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                 Subject = claims,
                 Expires = DateTime.UtcNow.AddDays(_jWtSettings.ExpirationInDays),
                 SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
