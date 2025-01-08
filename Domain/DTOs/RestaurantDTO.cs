namespace Domain.DTOs;

public record RestaurantDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Rating { get; set; }
    public string WorkingHours { get; set; }
    public string Description { get; set; }
    public string ContactPhone { get; set; }
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public string DeliveryPrice { get; set; }
}

public record CreateRestaurantDTO : RestaurantDTO;

public record UpdateRestaurantDTO : RestaurantDTO
{
    public int Id { get; set; }
}

public record ReadRestaurantDTO : UpdateRestaurantDTO
{
    public List<UpdateMenuDTO> Menus { get; set; }
    public List<UpdateOrderDTO> Orders { get; set; }
}

public record GetTop10RestaurantsForOrderCountInThisMonth
{
    public UpdateRestaurantDTO Type { get; set; }
    public int OrderCount { get; set; }
}