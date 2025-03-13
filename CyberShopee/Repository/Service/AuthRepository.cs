using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Models.DTO;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CyberShopee.Repository.Service
{
    public class AuthRepository : IAuthRepo
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Customer> _passwordHasher;
        public AuthRepository(AppDbContext context) {
            _context = context; 
            _passwordHasher = new PasswordHasher<Customer>();
        }

        public AuthResponseModel? Login(LoginModel login)
        {
            var customer = _context.Customers
                .FirstOrDefault(x => x.Email == login.Email);

            if (customer == null)
                return null;

            // Verify hashed password
            var result = _passwordHasher.VerifyHashedPassword(customer, customer.Password, login.Password);
            if (result != PasswordVerificationResult.Success) return null;

            string token = GenerateJwtToken(customer);

            return new AuthResponseModel
            {
                CustomerId = customer.CustomerId,
                Email = customer.Email,
                UserRole = customer.UserRole,
                Token = token
            };
        }

        private string GenerateJwtToken(Customer existing)
        {
            // Code to set Claim
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, existing.Email),
                new Claim(ClaimTypes.Role, existing.UserRole)
            };

            // Code to generate the token
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsMySecreteKeyForCyberShopee"));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7043",
                audience: "https://localhost:7043",
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signInCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
    }
}
