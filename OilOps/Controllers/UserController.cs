using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilOps.Models;
using OilOps.Models.DTO;
using OilOps.Services;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    //GET: api/users
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        var usersDto = users.Select(user => new UserDTO
        {
            FullName = user.FullName,
            DNI = user.DNI,
            UserName = user.UserName,
            UserType = user.UserType
        });
        return Ok(usersDto);
    }

    // GET: api/users/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(User user)
    { 
        await _userService.AddUser(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, User updatedUser)
    {
        var user = await _userService.GetUserById(id);
        if (user == null) return NotFound();
        user.FullName = updatedUser.FullName;
        user.DNI = updatedUser.DNI;
        user.UserName = updatedUser.UserName;
        user.Password = updatedUser.Password;
        user.UserType = updatedUser.UserType;
        _userService.UpdateUser(user);
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        if (user.UserType == 1)
        { 
            await _userService.DeleteUser(id);
        }
        return NoContent();
    }  
}