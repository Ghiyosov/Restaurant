using Domain.DTOs;
using Infrastructure.ApiResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;
[ApiController]
[Route("[controller]")]
public class AnalyticsController(IAnalyticsQuery _service)
{
    [HttpGet("Task11GetTop10RestaurantsForOrderCountInThisMonth")]
    public async Task<Responce<List<GetTop10RestaurantsForOrderCountInThisMonth>>> Task11GetTop10RestaurantsForOrderCountInThisMonth()
        => await _service.Task11GetTop10RestaurantsForOrderCountInThisMonth();
    
    [HttpGet("Task12GetTop10RevenueOfRestaurantInWeek")]
    public async Task<Responce<List<GetRevenueOfRestaurantInWeek>>> Task12GetTop10RevenueOfRestaurantInWeek()
        => await _service.Task12GetTop10RevenueOfRestaurantInWeek();
    
    [HttpGet("Task13GetRestaurantWhithTop3Menus")]
    public async Task<Responce<List<GetRestaurantWhithTop3Menus>>> Task13GetRestaurantWhithTop3Menus()
        => await _service.Task13GetRestaurantWhithTop3Menus();
    
    [HttpGet("Task14FindPeakHoursForOrders")]
    public async Task<Responce<List<FindPeakHoursForOrders>>> Task14FindPeakHoursForOrders()
        => await _service.Task14FindPeakHoursForOrders();
    
    [HttpGet("Task15GetAvgPriceOrderForAddress")]
    public async Task<Responce<List<GetAvgPriceOrderForAddress>>> Task15GetAvgPriceOrderForAddress()
        => await _service.Task15GetAvgPriceOrderForAddress();
    
    [HttpGet("Task16GetAverageDeliveryTimeInRegion")]
    public async Task<Responce<List<GetAverageDeliveryTimeInRegion>>> Task16GetAverageDeliveryTimeInRegion()
        => await _service.Task16GetAverageDeliveryTimeInRegion();
    
    [HttpGet("Task17GetUsersTopMenusCategory")]
    public async Task<Responce<List<GetUsersTopMenusCategory>>> Task17GetUsersTopMenusCategory()
        => await _service.Task17GetUsersTopMenusCategory();
    
    [HttpGet("Task18GetUsersDoMore10Orders")]
    public async Task<Responce<List<UpdateUserDTO>>> Task18GetUsersDoMore10Orders()
        => await _service.Task18GetUsersDoMore10Orders();
    
    [HttpGet("Task19GetCourierDeliveryTimeAndRating")]
    public async Task<Responce<List<GetCourierDeliveryTimeAndRating>>> Task19GetCourierDeliveryTimeAndRating()
        => await _service.Task19GetCourierDeliveryTimeAndRating();
    
    [HttpGet("Task20GetCourierEarnings")]
    public async Task<Responce<List<GetCourierEarnings>>> Task20GetCourierEarnings()
        => await _service.Task20GetCourierEarnings();
    

}