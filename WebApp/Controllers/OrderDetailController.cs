using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderDetailController(IOrderDetailService _service)
{
    [HttpGet("GetOrderDetails")]
    public async Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetails()
        => await _service.GetOrderDetails();
    
    [HttpGet("GetOrderDetailsById/{id}")]
    public async Task<Responce<ReadOrderDetailDTO>> GetOrderDetailsById(int id)
        => await _service.GetOrderDetailsById(id);
    
    [HttpGet("GetOrderDetailsByOrderId/{orderId}")]
    public async Task<Responce<List<ReadOrderDetailDTO>>> GetOrderDetailsByOrderId(int orderId)
        => await _service.GetOrderDetailsByOrderId(orderId);
    
    [HttpPost("CreateOrderDetail")]
    public async Task<Responce<string>> CreateOrderDetail(CreateOrderDetailDTO orderDetail)
        => await _service.CreateOrderDetail(orderDetail);
    
    [HttpPut("UpdateOrderDetail")]
    public async Task<Responce<string>> UpdateOrderDetail(UpdateOrderDetailDTO orderDetail)
        => await _service.UpdateOrderDetail(orderDetail);
    
    [HttpDelete("DeleteOrderDetail/{id}")]
    public async Task<Responce<string>> DeleteOrderDetail(int id)
        => await _service.DeleteOrderDetail(id);
}