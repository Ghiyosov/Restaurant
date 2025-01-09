using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(Context _context, IMapper _mapper) : IOrderService
{
    public async Task<Responce<List<ReadOrderDTO>>> GetOrders()
    {
        var ss = await _context.Orders.Include(x=>x.Courier).ToListAsync();
        var orders = _mapper.Map<List<ReadOrderDTO>>(ss);
        return new Responce<List<ReadOrderDTO>>(orders);
    }

    public async Task<Responce<ReadOrderDTO>> GetOrdersById(int id)
    {
        var x = await _context.Orders.Include(x=>x.Courier)
            .FirstOrDefaultAsync(x=>x.Id == id);
        if (x == null)
            return new Responce<ReadOrderDTO>(HttpStatusCode.NotFound, "Order not found");
        var order = _mapper.Map<ReadOrderDTO>(x);
        return new Responce<ReadOrderDTO>(order);
    }

    public async Task<Responce<string>> CreateOrder(CreateOrderDTO order)
    {
        var ord = _mapper.Map<Order>(order);
        await _context.Orders.AddAsync(ord);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Order created");
    }

    public async Task<Responce<string>> UpdateOrder(UpdateOrderDTO order)
    {
        var x = await _context.Orders.Include(x=>x.Courier)
            .FirstOrDefaultAsync(x=>x.Id == order.Id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Order not found");
       _mapper.Map(order, x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Order updated");
    }

    public async Task<Responce<string>> DeleteOrder(int id)
    {
        var x = await _context.Orders.Include(x=>x.Courier)
            .FirstOrDefaultAsync(x=>x.Id == id);
        if (x == null)
            return new Responce<string>(HttpStatusCode.NotFound, "Order not found");
        _context.Orders.Remove(x);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.OK, "Order deleted");
    }

    public async Task<Responce<List<GetCountOrdersInEveryoneStatus>>> Task3GetCountOrdersInEveryoneStatus()
    {
        var ss = _context.Orders.Include(x => x.Courier).AsQueryable();
        var res = await ss.GroupBy(x => x.OrderStatus)
            .Select(x => new GetCountOrdersInEveryoneStatus()
            {
                Status = x.Key.ToString(),
                Count = x.Count()
            }).ToListAsync();
        return new Responce<List<GetCountOrdersInEveryoneStatus>>(res);
    }

    public async Task<Responce<List<GetUsersAndOrdersCount>>> Task5GetUsersAndOrdersCount()
    {
        var us = await _context.Users.Include(x => x.Orders).ToListAsync();
        var res = us.Select(x => new GetUsersAndOrdersCount()
        {
            OrderCount = x.Orders.Count(),
            User = _mapper.Map<UpdateUserDTO>(x),
        }).ToList();
        return new Responce<List<GetUsersAndOrdersCount>>(res);
    }

    public async Task<Responce<List<UpdateOrderDTO>>> Task6GetOrdersByCourierId(int courierId)
    {
        var us = await _context.Orders.Include(x => x.Courier).Where(x => x.CourierId == courierId).ToListAsync();
        if (us == null)
            return new Responce<List<UpdateOrderDTO>>(HttpStatusCode.NotFound, "Order not found");
        var orders = _mapper.Map<List<UpdateOrderDTO>>(us);
        return new Responce<List<UpdateOrderDTO>>(orders);
    }

    public async Task<Responce<decimal>> Task7GetSumOrdersPriceInThisDay()
    {
        var ss = await _context.Orders.Where(x=>x.CreatedAt.Day == DateTime.Now.Day).ToListAsync();
        var sum = ss.Sum(x => x.TotalAmount);
        return new Responce<decimal>(sum);
    }

    public async Task<Responce<List<UpdateOrderDTO>>> Task7GetOrdersWhereOrderPriceSumMoreOrderPriceAvg()
    {
        var ss = await _context.Orders.Include(x=>x.OrderDetails).ToListAsync();
        var avg = ss.Average(x => x.TotalAmount);
        var res = ss.Where(x => x.TotalAmount > avg).ToList();
        var orders = _mapper.Map<List<UpdateOrderDTO>>(res);
        return new Responce<List<UpdateOrderDTO>>(orders);
    }
}