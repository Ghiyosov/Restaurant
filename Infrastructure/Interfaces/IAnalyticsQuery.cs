using Domain.DTOs;
using Infrastructure.ApiResponses;

namespace Infrastructure.Interfaces;

public interface IAnalyticsQuery
{
    public Task<Responce<List<GetTop10RestaurantsForOrderCountInThisMonth>>> Task11GetTop10RestaurantsForOrderCountInThisMonth();
    public Task<Responce<List<GetRevenueOfRestaurantInWeek>>> Task12GetTop10RevenueOfRestaurantInWeek();
    public Task<Responce<List<GetRestaurantWhithTop3Menus>>> Task13GetRestaurantWhithTop3Menus();
    public Task<Responce<List<FindPeakHoursForOrders>>> Task14FindPeakHoursForOrders();
    public Task<Responce<List<GetAvgPriceOrderForAddress>>> Task15GetAvgPriceOrderForAddress();
    public Task<Responce<List<GetAverageDeliveryTimeInRegion>>> Task16GetAverageDeliveryTimeInRegion();
    public Task<Responce<List<GetUsersTopMenusCategory>>> Task17GetUsersTopMenusCategory();
    public Task<Responce<List<UpdateUserDTO>>> Task18GetUsersDoMore10Orders();
    public Task<Responce<List<GetCourierDeliveryTimeAndRating>>> Task19GetCourierDeliveryTimeAndRating();
    public Task<Responce<List<GetCourierEarnings>>> Task20GetCourierEarnings();
}