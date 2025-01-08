using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IOrderDetailService
{
    public Task<Responce<List<OrderDetail>>> GetOrderDetails();
    public Task<Responce<OrderDetail>> GetOrderDetailsById(int id);
    public Task<Responce<List<OrderDetail>>> GetOrderDetailsByOrderId(int orderId);
    public Task<Responce<string>> CreateOrderDetail(CreateOrderDetailDTO orderDetail);
    public Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDTO orderDetail);
    public Task<Responce<string>> DeleteOrderDetail(int id);
}

