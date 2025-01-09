using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IOrderDetailService
{
    public Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetails();
    public Task<Responce<ReadOrderDetailDTO>> GetOrderDetailsById(int id);
    public Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetailsByOrderId(int orderId);
    public Task<Responce<string>> CreateOrderDetail(CreateOrderDetailDTO orderDetail);
    public Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDTO orderDetail);
    public Task<Responce<string>> DeleteOrderDetail(int id);
}

