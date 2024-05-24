using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using upLiftUnity_API.Models;
using BCrypt.Net;
using upLiftUnity_API.Services;
using upLiftUnity_API.Repositories.UserRepository;
using Microsoft.AspNetCore.Authorization;
using upLiftUnity_API.Services.EmailSender;

namespace upLiftUnity_API.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase

    {
        private readonly ILogger<UserController> _logger;
        private readonly APIDbContext _context;
        private readonly IUserRepository _user;
        private readonly IEmailSender _emailSender;


        public UserController(ILogger<UserController> logger, APIDbContext _dbcontext, IUserRepository _dbuser, IEmailSender emailSender)
        {
            _logger = logger;
            _context = _dbcontext;
            _user = _dbuser;
            _emailSender = emailSender;

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
            var email = Request.Email;

            var token = TokenService.GenerateToken(user.Id, roleName,email);

           
            return Ok(new
            {
                IsAuthenticated = true,
                Role = roleName,
                Token = token
            });
        }
        private void LogUserActivity(int userId, string ipAddress)
        {
            var userActivity = new Conversation
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
            string subject = "Informacione rreth kyçjes në Sistem";
            string message = $"Përshëndetje {user.Name + user.Surname},\n\n" +
                             $"Ju lutem gjeni kredencialet tuaja për hyrje në sistem, bashkë me linkun për regjistrim:\n\n" +
                             $"- Emaili: {user.Email}\n" +
                             $"- Fjalëkalimi: {user.Password}\n\n" +
                             $"Ju lutemi klikoni në linkun e mëposhtëm për të filluar procesin e regjistrimit:\n" +
                             $"http://localhost:8080/#/login\n\n" +
                             $"Nëse keni ndonjë pyetje ose problem gjatë procesit të kyçjes, mos hezitoni të na kontaktoni.\n\n" +
                             $"Me respekt,\n" +
                             $"upLiftUnity";


            var hashedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

            user.Password = hashedPass;

            _context.Users.Add(user);
            _context.SaveChanges();



            _emailSender.SendEmailAsync(user.Email, subject, message);


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

        [HttpPost]
        [Route("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            return Ok(await _user.ChangePassword(request.UserId, request.OldPassword, request.NewPassword));
        }

        public class ChangePasswordRequest
        {
            public int UserId { get; set; }
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
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




