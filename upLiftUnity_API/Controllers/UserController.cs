using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;

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
