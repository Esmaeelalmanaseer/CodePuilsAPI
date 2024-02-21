using CodePuilsAPI.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodePuilsAPI.Service.Body
{
    public class TokenService : ITokenServicecs
    {
        private IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
                this._configuration = configuration;
        }
        public string createToken(IdentityUser user, List<string> Role)
        {
            //create Claims
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),

            };
            claims.AddRange(Role.Select(role => new Claim(ClaimTypes.Role, role)));

            //JwtSecurity Token Parametrs
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );
            //return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
