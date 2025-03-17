using CyberShopee.Data;
using CyberShopee.Models;
using CyberShopee.Models.DTO;
using CyberShopee.Repository.DAO;
using CyberShopee.Repository.JWT;
using Microsoft.AspNetCore.Identity;

namespace CyberShopee.Repository.Service
{
    public class AdminRepository : IAdminRepo
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Admin> _passwordHasher;
        private readonly IConfiguration _configuration;
        public AdminRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Admin>();
        }

        public AuthResponseModel? Login(LoginModel login)
        {
            var admin = _context.Admin
                .FirstOrDefault(x => x.Email == login.Email);

            if (admin == null)
                return null;

            // Verify hashed password
            var result = _passwordHasher.VerifyHashedPassword(admin, admin.Password, login.Password);
            if (result != PasswordVerificationResult.Success) return null;

            Token t = new Token(_configuration);
            string token = t.GenerateJwtToken(admin);

            return new AuthResponseModel
            {
                Id = admin.AdminId,
                Email = admin.Email,
                UserRole = admin.UserRole,
                Token = token
            };
        }
    }
}
