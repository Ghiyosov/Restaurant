using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RestaurantService(Context _context, IMapper _mapper) : IRestaurantService
{
    
    public async Task<Responce<List<ReadRestaurantDTO>>> GetRestaurants()
    {
        var ss = await
            _context.Restaurants
                .Include(r => r.Menus)
                .Include(o => o.Orders).ToListAsync();
        var restaurants = _mapper.Map<List<ReadRestaurantDTO>>(ss);
        return new Responce<List<ReadRestaurantDTO>>(restaurants);
    }

    public async Task<Responce<ReadRestaurantDTO>> GetRestaurantsById(int id)
    {
        var x = await 
            _context.Restaurants
                .Include(r => r.Menus)
                .Include(o => o.Orders).FirstOrDefaultAsync();
        if (x == null)
            return new Responce<ReadRestaurantDTO>(HttpStatusCode.NotFound, "Restaurant not found");
        var restaurant = _mapper.Map<ReadRestaurantDTO>(x);
        return new Responce<ReadRestaurantDTO>(restaurant);
    }

    public async Task<Responce<string>> CreateRestaurant(CreateRestaurantDTO restaurant)
    {
        var rest = _mapper.Map<Restaurant>(restaurant);
        await _context.Restaurants.AddAsync(rest);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Restaurant created");
    }

    public async Task<Responce<string>> UpdateRestaurant(UpdateRestaurantDTO restaurant)
    {
        var x =  await _context.Restaurants.FirstOrDefaultAsync(r=>r.Id==restaurant.Id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Restaurant not found");
       _mapper.Map(restaurant, x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Restaurant updated");
    }

    public async Task<Responce<string>> DeleteRestaurant(int id)
    {
        var x =  await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Restaurant not found");
        _context.Restaurants.Remove(x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Restaurant updated");
    }

    public async Task<Responce<List<ReadRestaurantDTO>>> Task1GetTopActivRestaurantsSortByRating()
    {
        var ss =  _context.Restaurants
            .Include(r => r.Menus)
            .Include(o => o.Orders).
            Where(x => x.IsActive).OrderByDescending(z => z.Rating).Take(10).AsQueryable();
        var restaurants = _mapper.Map<List<ReadRestaurantDTO>>(ss);
        return new Responce<List<ReadRestaurantDTO>>(restaurants);
    }
    // public async Task<Responce<List<ReadRestaurantDTO>>> GetRestaurants()
    // {
    //     var ss =
    //         _context.Restaurants
    //             .Include(r => r.Menus)
    //             .Include(o => o.Orders).AsQueryable();
    //     var restaurants = ss.Select(x => new ReadRestaurantDTO()
    //     {
    //         Id = x.Id,
    //         Name = x.Name,
    //         Address = x.Address,
    //         Rating = x.Rating,
    //         WorkingHours = x.WorkingHours,
    //         Description = x.Description,
    //         ContactPhone = x.ContactPhone,
    //         IsActive = x.IsActive,
    //         Orders = x.Orders.Select(o => new UpdateOrderDTO()
    //         {
    //             Id = o.Id,
    //             UserId = o.UserId,
    //             RestaurantId = o.RestaurantId,
    //             OrderStatus = o.OrderStatus,
    //             CreatedAt = o.CreatedAt,
    //             DeliveredAt = o.DeliveredAt,
    //             TotalAmount = o.TotalAmount,
    //             DeliveryAddress = o.DeliveryAddress,
    //             PaymentMethod = o.PaymentMethod,
    //             PaymentStatus = o.PaymentStatus
    //         }).ToList(),
    //         Menus = x.Menus.Select(m => new UpdateMenuDTO()
    //         {
    //             Id = m.Id,
    //             Name = m.Name,
    //             Description = m.Description,
    //             Price = m.Price,
    //             Category = m.Category,
    //             IsAvailable = m.IsAvailable,
    //             PreparationTime = m.PreparationTime,
    //             Weight = m.Weight,
    //             PhotoUrl = m.PhotoUrl
    //         }).ToList()
    //     }).ToList();
    //     return new Responce<List<ReadRestaurantDTO>>(restaurants);
    // }
}