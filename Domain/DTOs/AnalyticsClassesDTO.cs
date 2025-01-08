namespace Domain.DTOs;

public record AnalyticsClassesDTO;

public record GetRevenueOfRestaurantInWeek
{
    public UpdateRestaurantDTO Restaurant { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
    public int OrderCount { get; set; }
}

public record GetRestaurantWhithTop3Menus
{
    public UpdateRestaurantDTO Restaurant { get; set; }
    public List<UpdateMenuDTO> Menus { get; set; }
}

public record FindPeakHoursForOrders
{
    public int Hours { get; set; }
    public int OrderCount { get; set; }
}

public record GetAvgPriceOrderForAddress
{
    public string Address { get; set; }
    public int OrderCount { get; set; }
}

public record GetAverageDeliveryTimeInRegion
{
    public string Region { get; set; }
    public TimeSpan Delivery { get; set; }
}

public record GetUsersTopMenusCategory
{
    public UpdateUserDTO User { get; set; }
    public string Category { get; set; }
}

public record GetCourierDeliveryTimeAndRating
{
    public UpdateCourierDTO Courier { get; set; }
    public decimal Rating { get; set; }
    public TimeSpan AvgDeliveryTime { get; set; }
}

public record GetCourierEarnings
{
    public UpdateCourierDTO Courier { get; set; }
    public decimal Earnings { get; set; }
}