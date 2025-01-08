using Domain.DTOs;
using Domain.Entities;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    public Task<Responce<List<ReadOrderDTO>>> GetOrders();
    public Task<Responce<ReadOrderDTO>> GetOrdersById(int id);
    public Task<Responce<string>> CreateOrder(CreateOrderDTO order);
    public Task<Responce<string>> UpdateOrder(UpdateOrderDTO order);
    public Task<Responce<string>> DeleteOrder(int id);
    public Task<Responce<List<GetCountOrdersInEveryoneStatus>>> Task3GetCountOrdersInEveryoneStatus();
    public Task<Responce<List<GetUsersAndOrdersCount>>> Task5GetUsersAndOrdersCount();
    public Task<Responce<List<UpdateOrderDTO>>> Task6GetOrdersByCourierId(int courierId);
    public Task<Responce<decimal>> Task7GetSumOrdersPriceInThisDay();
    public Task<Responce<List<UpdateOrderDTO>>> Task8GetOrdersWhereOrderPriceSumMoreOrderPriceAvg();
}