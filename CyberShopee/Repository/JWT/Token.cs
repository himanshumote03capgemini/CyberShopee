using CyberShopee.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CyberShopee.Repository.JWT
{
    public class Token
    {

        private readonly IConfiguration _configuration;

        public Token(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(dynamic existing)
        {
            // Code to set Claim
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, existing.Email),
                new Claim(ClaimTypes.Role, existing.UserRole)
            };

            // Code to generate the token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySecreteKeyForCyberShopee"));
            //var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7043",
                //issuer: _configuration["JWT:Issuer"],
                audience: "https://localhost:7043",
                //audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signInCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
