using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class UserService(Context _context, IMapper _mapper) : IUserService
{
    public async Task<Responce<List<ReadUserDTO>>> GetUsers()
    {
        var ss = await _context.Users
            .Include(x=>x.Orders)
            .Include(x=>x.Orders).ToListAsync();
        var users = _mapper.Map<List<ReadUserDTO>>(ss);
        return new Responce<List<ReadUserDTO>>(users);
    }

    public async Task<Responce<ReadUserDTO>> GetUser(int id)
    {
        var ss = await _context.Users
            .Include(x=>x.Orders)
            .Include(x=>x.Orders).FirstOrDefaultAsync(x => x.Id == id);
        if (ss == null)
            return new Responce<ReadUserDTO>(HttpStatusCode.NotFound, "No user was found" );
        var user = _mapper.Map<ReadUserDTO>(ss);
        return new Responce<ReadUserDTO>(user);
    }

    public async Task<Responce<string>> CreateUser(CreateUserDTO user)
    {
        var ss = _mapper.Map<User>(user);
        _context.Users.Add(ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "User Created");
    }

    public async Task<Responce<string>> UpdateUser(UpdateUserDTO user)
    {
        var ss = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "No user was found" );
        _mapper.Map(user, ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "User Updated");
    }

    public async Task<Responce<string>> DeleteUser(int id)
    {
        var ss = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "No user was found" );
        _context.Users.Remove(ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "User Deleted");
    }
}