using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using BCrypt.Net;
using upLiftUnity_API.Services;

namespace upLiftUnity_API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase

    {
        private readonly ILogger<UserController> _logger;
        private readonly APIDbContext _context;


        public UserController(ILogger<UserController> logger, APIDbContext _dbcontext)
        {
            _logger = logger;
            _context = _dbcontext;
        }
        [HttpPost("/login")]
        public IActionResult Login([FromForm] UserLogin Request)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == Request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(Request.Password, user.Password))
            {
                return Unauthorized("Mejli ose fjalkalimi eshte gabim");
            }

            var token = TokenService.GenerateToken(user.Id);

            // Shfaqni vetëm një mesazh në console për tokenin
            Console.WriteLine("Tokeni i gjeneruar: " + token);

            return Ok();
        }

        public class UserLogin { 
            public string Email { get; set;}
            public string Password { get; set;}
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                return Conflict("Ky mejl është tashmë i regjistruar.");
            }

            var hashedPass=BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = hashedPass;

            _context.Users.Add(user);
            _context.SaveChanges();

            // Kthejë një përgjigje të suksesshme
            return Ok();
        }

    }
}
