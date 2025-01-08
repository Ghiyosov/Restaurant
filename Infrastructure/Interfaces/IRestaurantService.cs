using Domain.DTOs;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IRestaurantService
{
    public Task<Responce<List<ReadRestaurantDTO>>> GetRestaurants();
    public Task<Responce<ReadRestaurantDTO>> GetRestaurantsById(int id);
    public Task<Responce<string>> CreateRestaurant(CreateRestaurantDTO restaurant);
    public Task<Responce<string>> UpdateRestaurant(UpdateRestaurantDTO restaurant);
    public Task<Responce<string>> DeleteRestaurant(int id);
    public Task<Responce<List<ReadRestaurantDTO>>> Task1GetTopActivRestaurantsSortByRating();
}