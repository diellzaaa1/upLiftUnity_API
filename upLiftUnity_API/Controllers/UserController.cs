using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using BCrypt.Net;
using upLiftUnity_API.Services;
using upLiftUnity_API.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;

namespace upLiftUnity_API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase

    {
        private readonly ILogger<UserController> _logger;
        private readonly APIDbContext _context;
        private readonly IUserRepository _user;


        public UserController(ILogger<UserController> logger, APIDbContext _dbcontext, IUserRepository _dbuser)
        {
            _logger = logger;
            _context = _dbcontext;
            _user = _dbuser;
        }
        [HttpPost("/login")]
        public IActionResult Login([FromBody] UserLogin Request)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == Request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(Request.Password, user.Password))
            {
                return Unauthorized("Mejli ose fjalkalimi eshte gabim");
            }
            var ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            LogUserActivity(user.Id, ipAddress);

            var roleName = _user.GetUserRole(user.RoleId);

            var token = TokenService.GenerateToken(user.Id, roleName);

            // Kthe një objekt anonim si përgjigje
            return Ok(new
            {
                IsAuthenticated = true,
                Role = roleName,
                Token = token
            });
        }
        private void LogUserActivity(int userId, string ipAddress)
        {
            var userActivity = new UserActivity
            {
                UserId = userId,
                IPAddress = ipAddress,
                LoginTime = DateTime.Now
            };

            _context.UserActivities.Add(userActivity);
            _context.SaveChanges();
        }


        public class UserLogin {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
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

            var hashedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = hashedPass;

            _context.Users.Add(user);
            _context.SaveChanges();

            // Kthejë një përgjigje të suksesshme
            return Ok();
        }
        [HttpGet]
        [Route("GetUsers")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Get()
        {
            var users = await _user.GetUsers();
            return Ok(users);
        }
        [HttpPut]
        [Route("UpdateUser")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Put(User user)
        {
            await _user.UpdateUser(user);
            return Ok("Updated Successfully");
        }
        [HttpDelete]
        //[HttpDelete("{id}")]
        [Route("DeleteUser")]
        [Authorize(Roles = "SuperAdmin")]
        public JsonResult Delete(int id)
        {
            _user.DeleteUser(id);
            return new JsonResult("Deleted Successfully");
        }

        [HttpGet]
        [Route("GetUserByID/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetUserByID(int Id)
        {
            return Ok(await _user.GetUserById(Id));
        }

        [HttpGet]
        [Route("GetUsersByRoleId")]
        public async Task<IActionResult> GetUsersByRoleId(int roleId)
        {
            var users = await _user.GetUsersByRoleId(roleId);
            return Ok(users);
        }

    }


  


}




