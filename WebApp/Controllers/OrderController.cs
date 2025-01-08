using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService _service)
{
    [HttpGet("GetOrders")]
    public async Task<Responce<List<ReadOrderDTO>>> GetOrders()
        => await _service.GetOrders();
    
    [HttpGet("GetOrder/{id}")]
    public async Task<Responce<ReadOrderDTO>> GetOrdersById(int id)
        => await _service.GetOrdersById(id);
    
    [HttpPost("CreateOrder")]
    public async Task<Responce<string>> CreateOrder(CreateOrderDTO order)
        => await _service.CreateOrder(order);
    
    [HttpPut("UpdateOrder")]
    public async Task<Responce<string>> UpdateOrder(UpdateOrderDTO order)
        => await _service.UpdateOrder(order);
    
    [HttpDelete("DeleteOrder/{id}")]
    public async Task<Responce<string>> DeleteOrder(int id)
        => await _service.DeleteOrder(id);

    [HttpGet("Task3GetCountOrdersInEveryoneStatus")]
    public async Task<Responce<List<GetCountOrdersInEveryoneStatus>>> Task3GetCountOrdersInEveryoneStatus()
        => await _service.Task3GetCountOrdersInEveryoneStatus();
    
    [HttpGet("Task5GetUsersAndOrdersCount")]
    public Task<Responce<List<GetUsersAndOrdersCount>>> Task5GetUsersAndOrdersCount()
        => _service.Task5GetUsersAndOrdersCount();
    
    [HttpGet("Task6GetUsersAndOrdersCount/{id}")]
    public async Task<Responce<List<UpdateOrderDTO>>> Task6GetOrdersByCourierId(int courierId)
        => await _service.Task6GetOrdersByCourierId(courierId);
    
    [HttpGet("Task7GetSumOrdersPriceInThisDay")]
    public async Task<Responce<decimal>> Task7GetSumOrdersPriceInThisDay()
        => await _service.Task7GetSumOrdersPriceInThisDay();
    
    [HttpGet("Task8GetOrdersWhereOrderPriceSumMoreOrderPriceAvg")]
    public async Task<Responce<List<UpdateOrderDTO>>> Task8GetOrdersWhereOrderPriceSumMoreOrderPriceAvg()
        => await _service.Task8GetOrdersWhereOrderPriceSumMoreOrderPriceAvg();
}