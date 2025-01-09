using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderDetailService(Context _context, IMapper _mapper) : IOrderDetailService
{
    public async Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetails()
    {
        var ss = await _context.OrderDetails
            .Include(x => x.Order)
            .Include(x=>x.Menu).ToListAsync();
        var orderDetails = _mapper.Map<List<ReadOrderDetailDTO>>(ss);
        return new Responce<List<ReadOrderDetailDTO>>(orderDetails);
    }

    public async Task<Responce<ReadOrderDetailDTO>> GetOrderDetailsById(int id)
    {
        var ss =  _context.OrderDetails
            .Include(x => x.Order)
            .Include(x=>x.Menu).FirstOrDefaultAsync(x => x.Id == id);
        if (ss == null)
            return new Responce<ReadOrderDetailDTO>(HttpStatusCode.NotFound, "OrderDetail not found" );
        var orderDetail = _mapper.Map<ReadOrderDetailDTO>(ss);
        return new Responce<ReadOrderDetailDTO>(orderDetail);
    }

    public async Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetailsByOrderId(int orderId)
    {
        var ss = await  _context.OrderDetails
            .Include(x => x.Order)
            .Include(x=>x.Menu)
            .Where(x=>x.Order.Id == orderId).ToListAsync();
        if (ss == null)
            return new Responce<List<ReadOrderDetailDTO>>(HttpStatusCode.NotFound, $"OrderDetail int Order ID:{orderId} not found" );
        var orderDetails = _mapper.Map<List<ReadOrderDetailDTO>>(ss);
        return new Responce<List<ReadOrderDetailDTO>>(orderDetails);
    }

    public async Task<Responce<string>> CreateOrderDetail(CreateOrderDetailDTO orderDetail)
    {
        var ss = _mapper.Map<OrderDetail>(orderDetail);
        await _context.OrderDetails.AddAsync(ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Order detail created");
    }

    public async Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDTO orderDetail)
    {
        var ss =  await _context.OrderDetails.FirstOrDefaultAsync(x=>x.Id==orderDetail.Id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "OrderDetail not found" );
        _mapper.Map(orderDetail, ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Order detail updated");
    }

    public async Task<Responce<string>> DeleteOrderDetail(int id)
    {
        var ss =  await _context.OrderDetails.FirstOrDefaultAsync(x=>x.Id==id);
        if (ss == null)
            return new Responce<string>(HttpStatusCode.NotFound, "OrderDetail not found" );
        _context.OrderDetails.Remove(ss);
        var result = await _context.SaveChangesAsync();
        return result == 0 
            ? new Responce<string>(HttpStatusCode.InternalServerError, "Internal Server Error")
            : new Responce<string>(HttpStatusCode.Created, "Order detail deleted");
    }
}