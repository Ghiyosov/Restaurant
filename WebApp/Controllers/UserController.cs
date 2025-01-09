using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController(IUserService _service)
{
    [HttpGet("GetUsers")]
    public async Task<Responce<List<ReadUserDTO>>> GetUsers()
        => await _service.GetUsers();
    
    [HttpGet("GetUser/{id}")]
    public async Task<Responce<ReadUserDTO>> GetUser(int id)
        => await _service.GetUser(id);
    
    [HttpPost("CreateUser")]
    public async Task<Responce<string>> CreateUser(CreateUserDTO user)
        => await _service.CreateUser(user);
    
    [HttpPut("UpdateUser")]
    public async Task<Responce<string>> UpdateUser(UpdateUserDTO user)
        => await _service.UpdateUser(user);
    
    [HttpDelete("DeleteUser/{id}")]
    public async Task<Responce<string>> DeleteUser(int id)
        => await _service.DeleteUser(id);
}