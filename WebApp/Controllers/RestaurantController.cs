using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class RestaurantController(IRestaurantService _service)
{
    [HttpGet("GetRestaurants")]
    public async Task<Responce<List<ReadRestaurantDTO>>> GetRestaurants()
        => await _service.GetRestaurants();

    [HttpGet("GetRestaurants/{id}")]
    public async Task<Responce<ReadRestaurantDTO>> GetRestaurantsById(int id)
        => await _service.GetRestaurantsById(id);
    
    [HttpPost("CreateRestaurant")]
    public async Task<Responce<string>> CreateRestaurant(CreateRestaurantDTO restaurant)
        => await _service.CreateRestaurant(restaurant);
    
    [HttpPut("UpdateRestaurant")]
    public async Task<Responce<string>> UpdateRestaurant(UpdateRestaurantDTO restaurant)
        => await _service.UpdateRestaurant(restaurant);
    
    [HttpDelete("DeleteRestaurant/{id}")]
    public async Task<Responce<string>> DeleteRestaurant(int id)
        => await _service.DeleteRestaurant(id);
    
    [HttpGet("Task1GetTopActivRestaurantsSortByRating")]
    public async Task<Responce<List<ReadRestaurantDTO>>> Task1GetTopActivRestaurantsSortByRating()
        => await _service.Task1GetTopActivRestaurantsSortByRating();
    
}