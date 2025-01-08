using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(Context _context) : IOrderService
{
    public async Task<Responce<List<ReadOrderDTO>>> GetOrders()
    {
        var ss = _context.Orders.Include(x=>x.Courier).AsQueryable();
        var orders = await ss.Select(x => new ReadOrderDTO
        {
            Id = x.Id,
            UserId = x.UserId,
            RestaurantId = x.RestaurantId,
            CourierId = x.CourierId,
            OrderStatus = x.OrderStatus,
            CreatedAt = x.CreatedAt,
            DeliveredAt = x.DeliveredAt,
            TotalAmount = x.TotalAmount,
            DeliveryAddress = x.DeliveryAddress,
            PaymentMethod = x.PaymentMethod,
            PaymentStatus = x.PaymentStatus,
            OrderDetails = x.OrderDetails.Select(x => new UpdateOrderDetailDTO()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                MenuItemId = x.MenuItemId,
                Quantity = x.Quantity,
                Price = x.Price,
                SpecialInstructions = x.SpecialInstructions
            }).ToList()
        }).ToListAsync();
        return new Responce<List<ReadOrderDTO>>(orders);
    }

    public async Task<Responce<ReadOrderDTO>> GetOrdersById(int id)
    {
        var x = await _context.Orders.Include(x=>x.Courier)
            .FirstOrDefaultAsync(x=>x.Id == id);
        if (x == null)
            return new Responce<ReadOrderDTO>(HttpStatusCode.NotFound, "Order not found");
        var order = new ReadOrderDTO()
        {
            Id = x.Id,
            UserId = x.UserId,
            RestaurantId = x.RestaurantId,
            CourierId = x.CourierId,
            OrderStatus = x.OrderStatus,
            CreatedAt = x.CreatedAt,
            DeliveredAt = x.DeliveredAt,
            TotalAmount = x.TotalAmount,
            DeliveryAddress = x.DeliveryAddress,
            PaymentMethod = x.PaymentMethod,
            PaymentStatus = x.PaymentStatus,
            OrderDetails = x.OrderDetails.Select(x => new UpdateOrderDetailDTO()
            {
                Id = x.Id,
                OrderId = x.OrderId,
                MenuItemId = x.MenuItemId,
                Quantity = x.Quantity,
                Price = x.Price,
                SpecialInstructions = x.SpecialInstructions
            }).ToList()
        };
        return new Responce<ReadOrderDTO>(order);
    }

    public async Task<Responce<string>> CreateOrder(CreateOrderDTO order)
    {
        var ord = new Order()
        {
            UserId = order.UserId,
            RestaurantId = order.RestaurantId,
            CourierId = order.CourierId,
            OrderStatus = order.OrderStatus,
            CreatedAt = order.CreatedAt,
            DeliveredAt = order.DeliveredAt,
            TotalAmount = order.TotalAmount,
            DeliveryAddress = order.DeliveryAddress,
            PaymentMethod = order.PaymentMethod,
            PaymentStatus = order.PaymentStatus,
        };
        _context.Orders.Add(ord);
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
        x.UserId = order.UserId;
        x.RestaurantId = order.RestaurantId;
        x.CourierId = order.CourierId;
        x.OrderStatus = order.OrderStatus;
        x.CreatedAt = order.CreatedAt;
        x.DeliveredAt = order.DeliveredAt;
        x.TotalAmount = order.TotalAmount;
        x.DeliveryAddress = order.DeliveryAddress;
        x.PaymentMethod = order.PaymentMethod;
        x.PaymentStatus = order.PaymentStatus;
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

    public Task<Responce<List<GetUsersAndOrdersCount>>> Task5GetUsersAndOrdersCount()
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<UpdateOrderDTO>>> Task6GetOrdersByCourierId(int courierId)
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<decimal>> Task7GetSumOrdersPriceInThisDay()
    {
        throw new NotImplementedException();
    }

    public async Task<Responce<List<UpdateOrderDTO>>> Task8GetOrdersWhereOrderPriceSumMoreOrderPriceAvg()
    {
        throw new NotImplementedException();
    }
}