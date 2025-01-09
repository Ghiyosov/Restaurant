using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CourierService(Context _context, IMapper _mapper):ICourierService
{
    public async Task<Responce<List<ReadCourierDTO>>> GetCouriers()
    {
        var ss = await _context.Couriers
            .Include(x => x.User)
            .Include(x => x.Orders).ToListAsync();
        var couriers = _mapper.Map<List<ReadCourierDTO>>(ss);
        return new Responce<List<ReadCourierDTO>>(couriers);
    }

    public async Task<Responce<ReadCourierDTO>> GetCourierById(int id)
    {
        var ss = await _context.Couriers
            .Include(x => x.User)
            .Include(x => x.Orders).FirstOrDefaultAsync(x => x.Id == id);
        if (ss == null)
            return new Responce<ReadCourierDTO>(HttpStatusCode.NotFound, "Courier not found");
        var couriers = _mapper.Map<ReadCourierDTO>(ss);
        return new Responce<ReadCourierDTO>(couriers);
    }

    public async Task<Responce<string>> CreateCourier(CreateCourierDTO createCourierDTO)
    {
        var courier = _mapper.Map<Courier>(createCourierDTO);
        await _context.Couriers.AddAsync(courier);
        var result = await _context.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Courier created");
    }

    public async Task<Responce<string>> UpdateCourier(UpdateCourierDTO updateCourierDTO)
    {
        var ss = await _context.Couriers.FirstOrDefaultAsync(x => x.Id == updateCourierDTO.Id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Courier not found");
        _mapper.Map(updateCourierDTO, ss);
        var result = await _context.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Courier Updated");
    }

    public async Task<Responce<string>> DeleteCourier(int id)
    {
        var ss = await _context.Couriers.FirstOrDefaultAsync(x => x.Id == id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Courier not found"); 
        _context.Couriers.Remove(ss);
        var result = await _context.SaveChangesAsync();
        return result == 0
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal server error")
            : new Responce<string>(HttpStatusCode.Created, "Courier Deleted");

    }

    public async Task<Responce<List<ReadCourierDTO>>> Task8GetTop5Couriers()
    {
        var ss = await  _context.Couriers
            .Include(x => x.User)
            .Include(x => x.Orders)
            .OrderByDescending(x=>x.Rating).Take(5).ToListAsync();
        var couriers = _mapper.Map<List<ReadCourierDTO>>(ss);
        return new Responce<List<ReadCourierDTO>>(couriers);
    }
}