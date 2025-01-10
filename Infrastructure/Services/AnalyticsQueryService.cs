using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AnalyticsQueryService(Context _context, IMapper _mapper): IAnalyticsQuery
{
    public async Task<Responce<List<GetTop10RestaurantsForOrderCountInThisMonth>>> Task11GetTop10RestaurantsForOrderCountInThisMonth()
    {
        var ss = _context.Restaurants
            .Include(x => x.Orders)
            .Select(x => new
            {
                Restaurant = x,
                OrderCount = x.Orders.Where(x => x.CreatedAt.Month == DateTime.Now.Month).Count(),
            }).OrderByDescending(x => x.OrderCount).Take(10);
        var resault = await ss.Select(x => new GetTop10RestaurantsForOrderCountInThisMonth()
        {
            Restaurant = _mapper.Map<UpdateRestaurantDTO>(x.Restaurant),
            OrderCount = x.OrderCount,
        }).ToListAsync();
        return new Responce<List<GetTop10RestaurantsForOrderCountInThisMonth>>(resault);
    }

    public async Task<Responce<List<GetRevenueOfRestaurantInWeek>>> Task12GetTop10RevenueOfRestaurantInWeek()
    {
        var ss = _context.Restaurants
            .Include(x => x.Orders).AsQueryable();
        // Текущая дата
        DateTime today = DateTime.Now;

        // Найти начало и конец последней недели
        int daysToLastMonday = (int)today.DayOfWeek == 0 ? 7 : (int)today.DayOfWeek; // Sunday = 7
        DateTime lastMonday = today.AddDays(-daysToLastMonday - 6); // Понедельник последней недели
        DateTime lastSunday = lastMonday.AddDays(6);               // Воскресенье последней недели
        
        var resault = await ss.Select(x => new GetRevenueOfRestaurantInWeek()
        {
            Restaurant = _mapper.Map<UpdateRestaurantDTO>(x),
            OrderCountInDay = x.Orders.Where(x => x.CreatedAt <= lastSunday && x.CreatedAt >= lastMonday)
                .GroupBy(x => x.CreatedAt.DayOfWeek)
                .Select(x => new OrderCountInDay()
                {
                    DayOfWeek = x.Key.ToString(),
                    OrderCount = x.Count(),
                }).ToList()
        }).ToListAsync();
        return new Responce<List<GetRevenueOfRestaurantInWeek>>(resault);
    }

    public async Task<Responce<List<GetRestaurantWhithTop3Menus>>> Task13GetRestaurantWhithTop3Menus()
    {
        var res = await _context.OrderDetails
            .Include(x => x.Order)
            .ThenInclude(x=>x.Restaurant)
            .GroupBy(od => new {od.Menu.Restaurant, od.Menu})
            .Select(x=> new
            {
                Restaurant = _mapper.Map<UpdateRestaurantDTO>(x.Key.Restaurant),
                Menu = _mapper.Map<UpdateMenuDTO>(x.Key.Menu),
                OrderCount = x.Sum(od=>od.Quantity)
            }).OrderByDescending(x=>x.OrderCount)
            .GroupBy(od => od.Restaurant)
            .Select(x=>new 
            {
                Restaurant = x.Key,
                Menus = x.Select(x=>x.Menu),
            }).ToListAsync();
        var s = res.Select(x => new GetRestaurantWhithTop3Menus()
        {
            Restaurant = _mapper.Map<UpdateRestaurantDTO>(x.Restaurant),
            Menus = _mapper.Map<List<UpdateMenuDTO>>(x.Menus),
        }).ToList();
        return new Responce<List<GetRestaurantWhithTop3Menus>>(s);
    }

    public async Task<Responce<List<FindPeakHoursForOrders>>> Task14FindPeakHoursForOrders()
    {
        var order = _context.Orders.AsQueryable();
        var res = await order.GroupBy(x => x.CreatedAt.Hour)
            .Select(x => new FindPeakHoursForOrders()
            {
                Hours = x.Key,
                OrderCount = x.Count(),
            }).ToListAsync();
        return new Responce<List<FindPeakHoursForOrders>>(res);
    }

    public async Task<Responce<List<GetAvgPriceOrderForAddress>>> Task15GetAvgPriceOrderForAddress()
    {
        var users =  _context.Users.Include(x => x.Orders).AsQueryable();
        var res = await users.GroupBy(x=>x.Address)
            .Select(x=> new GetAvgPriceOrderForAddress()
            {
                Address = x.Key,
                OrderCount = x.Select(x=>x.Orders.Count).Sum(),
            }).OrderByDescending(x=>x.OrderCount).ToListAsync();
        return new Responce<List<GetAvgPriceOrderForAddress>>(res);
    }

    public async Task<Responce<List<GetAverageDeliveryTimeInRegion>>> Task16GetAverageDeliveryTimeInRegion()
    {
        var order = _context.Orders
            .Include(x => x.OrderDetails)
            .Include(x=>x.User).AsQueryable();
        var res = await order.GroupBy(x => x.User.Address)
            .Select(x => new GetAverageDeliveryTimeInRegion()
            {
                Region = x.Key,
                Delivery = x.Where(x=>x.DeliveredAt!=null)
                    .Select(o =>  (o.DeliveredAt - o.CreatedAt).Minutes).Average()
            }).ToListAsync();
        return new Responce<List<GetAverageDeliveryTimeInRegion>>(res);
    }

    public async Task<Responce<List<GetUsersTopMenusCategory>>> Task17GetUsersTopMenusCategory()
    {
        var favoriteCategory = _context.Orders
            .Join(_context.OrderDetails, o => o.Id, od => od.OrderId, (o, od)
                => new
                {
                    o.UserId,
                    od.MenuItemId
                }).Join(_context.Menus, od => od.MenuItemId, m => m.Id,
                (od, m) => new
                {
                    od.UserId,
                    m.Category
                })
            .GroupBy(x => new { x.UserId, x.Category })
            .Select(g =>
                new
                {
                    UserId = g.Key.UserId,
                    Category = g.Key.Category,
                    TotalOrders = g.Count()
                })
            .GroupBy(x => x.UserId)
            .Select(g => new
            {
                UserId = g.Key,
                FavoriteCategory = g.OrderByDescending(c => c.TotalOrders)
                    .FirstOrDefault().Category
            }).AsQueryable();
        var res = await favoriteCategory
            .Select(x=>new GetUsersTopMenusCategory()
            {
                User = _mapper.Map<UpdateUserDTO>( _context.Users.FirstOrDefault(g=>g.Id == x.UserId)),
                Category = x.FavoriteCategory
            }).OrderByDescending(x=>x.Category).ToListAsync();
        return new Responce<List<GetUsersTopMenusCategory>>(res);
    }

    public async Task<Responce<List<UpdateUserDTO>>> Task18GetUsersDoMore10Orders()
    {
        var users =  _context.Users.Include(x => x.Orders).AsQueryable();
        var res = await users
            .Where(user => user.Orders.GroupBy(x => x.CreatedAt.Month)
                .Any(order=>order.Count() > 5))
            .Select(user => _mapper.Map<UpdateUserDTO>(user)).ToListAsync();
        return new Responce<List<UpdateUserDTO>>(res);
    }

    public async Task<Responce<List<GetCourierDeliveryTimeAndRating>>> Task19GetCourierDeliveryTimeAndRating()
    {
        var corier = _context.Couriers.Include(x=>x.Orders).AsQueryable();
        var res = await corier.Select(x=>new GetCourierDeliveryTimeAndRating()
        {
            Courier = _mapper.Map<UpdateCourierDTO>(x),
            AvgDeliveryTime = x.Orders.Select(x=>(x.DeliveredAt - x.CreatedAt).Minutes).Average(),
        }).OrderBy(x=>x.AvgDeliveryTime).ToListAsync();
        return new Responce<List<GetCourierDeliveryTimeAndRating>>(res);
    }

    public async Task<Responce<List<GetCourierEarnings>>> Task20GetCourierEarnings()
    {
        var corier = _context.Couriers.Include(x=>x.Orders).AsQueryable();
        var res = await corier.Select(x=>new GetCourierEarnings()
        {
            Courier = _mapper.Map<UpdateCourierDTO>(x),
            Earnings = x.Orders.Sum(x=>x.TotalAmount)
        }).OrderByDescending(x=>x.Earnings).ToListAsync();
        return new Responce<List<GetCourierEarnings>>(res);
    }
}