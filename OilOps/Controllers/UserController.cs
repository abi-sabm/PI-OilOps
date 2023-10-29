using Microsoft.AspNetCore.Mvc;
using OilOps.DTO;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    //GET: api/users
    [HttpGet]
    public IActionResult Get()
    {
        var users = _userRepository.GetAllUsers();
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
    public IActionResult Get(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public IActionResult Post(User user)
    {
        _userRepository.AddUser(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, User updatedUser)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null) return NotFound();
        user.FullName = updatedUser.FullName;
        user.DNI = updatedUser.DNI;
        user.UserName = updatedUser.UserName;
        user.Password = updatedUser.Password;
        user.UserType = updatedUser.UserType;
        _userRepository.UpdateUser(user);
        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = _userRepository.GetUserById(id);
        if (user == null) return NotFound();
        _userRepository.DeleteUser(id);
        return NoContent();
    }  
}