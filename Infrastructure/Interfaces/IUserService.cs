using Domain.DTOs;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    public Task<Responce<List<ReadUserDTO>>> GetUsers();
    public Task<Responce<ReadUserDTO>> GetUser(int id);
    public Task<Responce<string>> CreateUser(CreateUserDTO user);
    public Task<Responce<string>> UpdateUser(UpdateUserDTO user);
    public Task<Responce<string>> DeleteUser(int id);
}