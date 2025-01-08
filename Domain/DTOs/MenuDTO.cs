namespace Domain.DTOs;

public record MenuDTO
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public int PreparationTime	 { get; set; }
    public int Weight { get; set; }
    public string PhotoUrl { get; set; }
}

public record CreateMenuDTO : MenuDTO;

public record UpdateMenuDTO : MenuDTO
{
    public int Id { get; set; }
}

public record ReadMenuDTO : UpdateMenuDTO
{
    public UpdateRestaurantDTO Restaurant { get; set; }
}

public record GetMenusCategoryWithAveregPrice
{
    public string Category { get; set; }
    public decimal AvgPrice { get; set; }
}

public record GetCategoryHasMoreMenus
{
    public string Category { get; set; }
    public List<UpdateMenuDTO> Menus { get; set; }
}

