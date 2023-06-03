using Hackademy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Voltar.Common.Helper;

namespace Hackademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }
        private TokenHelper TokenHelper { get; set; }
        private CurrentUserService CurrentUserService { get; set; }
        public UserController(TokenHelper _tokenHelper, HackademyContext _hackademyContext, CurrentUserService currentUserService)
        {
            HackademyContext= _hackademyContext;
            TokenHelper = _tokenHelper;
            CurrentUserService = currentUserService;
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            return Ok(true);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest LoginRequest)
        {
            var user= HackademyContext.Users.FirstOrDefault(x => x.Email.Equals(LoginRequest.Email) && x.Password.Equals(LoginRequest.Password));
            if (user != null)
            {
                var token = TokenHelper.CreateJwtSecurityToken(new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                      new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
                },
                DateTime.Now.AddDays(30));
                    return Ok(token);
            }
            return BadRequest(false);
        }

        [HttpGet("GetUsers")]

        public async Task<IActionResult> GetUsers()
        {
            var users = HackademyContext.Users.ToList();
            return Ok(users);
        }
        [HttpPost("Registration")]
        [AllowAnonymous]
        public async Task<IActionResult> Registration([FromBody] RegistrationRequest RegistrationRequest)
        {
            var User = new Domain.Entity.User()
            {
                Bio = "",
                Birthdate = RegistrationRequest.Birthdate,
                City = RegistrationRequest.City,
                Email = RegistrationRequest.Email,
                Name = RegistrationRequest.Name,
                Password = RegistrationRequest.Password,
                Point = 0,
                Street = RegistrationRequest.Street,
                Surname = RegistrationRequest.Surname,
                UserId = 0,
                StreetNumber = RegistrationRequest.StreetNumber,
                Image = Guid.NewGuid()
            };
            if(HackademyContext.Users.Any(c=> c.Email.Equals(User.Email))){
                return BadRequest("Utente già presente");
            }
            HackademyContext.Users.Add(
              User) ;
            HackademyContext.SaveChanges();
            return Ok(User.UserId);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest UpdateUserRequest)
        {
            var User = HackademyContext.Users.FirstOrDefault(c => c.UserId == UpdateUserRequest.Id);
            if (User == null) {
                return BadRequest(false);
            }
            User.Name = UpdateUserRequest.Name;
            User.Surname = UpdateUserRequest.Surname;
            User.Email = UpdateUserRequest.Email;
            User.Birthdate = UpdateUserRequest.Birthdate;
            User.City = UpdateUserRequest.City;
            User.Street = UpdateUserRequest.Street;
            User.StreetNumber = UpdateUserRequest.StreetNumber;
            User.Bio = UpdateUserRequest.Bio;

            HackademyContext.Users.Update(User);
            HackademyContext.SaveChanges();
            return Ok(true);
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] int Id)
        {

            var User = HackademyContext.Users.FirstOrDefault(c => c.UserId == Id);
            if (User == null)
            {

                return BadRequest(false);

            }

            HackademyContext.Users.Remove(User);
            HackademyContext.SaveChanges();
            return Ok(true);
        }

    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegistrationRequest
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
    }
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string Bio { get; set; }
    }
}
