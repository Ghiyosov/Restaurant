using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Restaurant
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Rating { get; set; }
    public string WorkingHours { get; set; }
    public string Description { get; set; }
    [Phone]
    public string ContactPhone { get; set; }
    public bool IsActive { get; set; }
    public decimal MinOrderAmount { get; set; }
    public string DeliveryPrice { get; set; }

    [ForeignKey("RestaurantId")]
    public List<Menu> Menus { get; set; }
    [ForeignKey("RestaurantId")]
    public List<Order> Orders { get; set; }
}