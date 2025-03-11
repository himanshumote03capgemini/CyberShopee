using CyberShopee.Models.DTO;
using CyberShopee.Repository.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberShopee.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepo _repository;
        public AuthController(IAuthRepo repository)
        {
            _repository = repository;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var response = _repository.Login(login);

            if (response == null)
                return Unauthorized("Invalid credentials");

            return Ok(response);
        }
    }
}
