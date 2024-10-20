using JalilApiSecurity.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JalilApiSecurity.Service
{
    public class TokenService
    { 
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(AppUser usr,string role,DateTime expireDate)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDesc = new SecurityTokenDescriptor()
            {
                Audience = audience,
                Issuer = issuer,
                Expires = expireDate,
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new List<Claim>()
    {
         new("Id", usr.Id),

         new(JwtRegisteredClaimNames.Sub, usr.UserName ?? "NoName"),
         new(JwtRegisteredClaimNames.Email,usr.Email?? "NoEmail"),
         new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new ("BrithDate",usr.Brithdate.ToString()),
         new ("FullNaame",usr.FirstName+" "+ usr.LastName),
         new (ClaimTypes.Role, role)
    })
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDesc);
            var strToken = handler.WriteToken(token);
            return strToken;
        }
        
    }
    
}
