using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models.Dto.CreationDto;
using NotesAPI.Models.Dto.Data;
using NotesAPI.Models.Dto;
using NotesAPI.Repository.Interfaces;
using NotesAPI.Models.Dto.LoginDto;

namespace NotesAPI.Controllers
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById([FromRoute] int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<int> LoginUser([FromBody] UserLoginDto dto)
        {
            var userId = _userService.LoginUser(dto.Email, dto.Password);
            return Ok(userId);
        }

        [HttpPost]
        public ActionResult AddUser([FromBody] CreateUserDto dto)
        {
            var userId = _userService.AddUser(dto);
            return Created($"/api/user/{userId}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromRoute] int id)
        {
            _userService.DeleteUser(id);
            return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser([FromRoute] int id, [FromBody] UserDataDto dto)
        {
            _userService.UpdateUser(id, dto);
            return Ok();
        }
    }
}
