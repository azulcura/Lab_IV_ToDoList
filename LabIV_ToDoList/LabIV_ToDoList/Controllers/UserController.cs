using LabIV_ToDoList.Models;
using LabIV_ToDoList.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabIV_ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userServices.GetAllUsers();

            foreach (var user in users)
            {
                user.TodoItems = _userServices.GetTodoItemsByUserId(user.id);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userServices.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(UserDto user)
        {
            var newUser = _userServices.AddUser(user);

            var userResponse = new
            {
                id = newUser.id,
                name = newUser.name,
                email = newUser.email,
                address = newUser.address
            };

            return CreatedAtAction(nameof(GetUser), new { id = newUser.id }, userResponse);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var updatedUser = _userServices.UpdateUser(id, userDto);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var result = _userServices.DeleteUser(id);
            if (result == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
