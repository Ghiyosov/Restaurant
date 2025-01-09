using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Menu
{
    [Key]
    public int Id { get; set; }

    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
    public int PreparationTime	 { get; set; }
    public int Weight { get; set; }
    [Url]
    public string PhotoUrl { get; set; }

    public Restaurant Restaurant { get; set; }
    [ForeignKey("MenuItemId")] 
    public List<OrderDetail> OrderDetails { get; set; }
}