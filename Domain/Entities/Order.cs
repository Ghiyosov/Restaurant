using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int CourierId { get; set; }
    public int OrderStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeliveredAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryAddress	 { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    
    public User User { get; set; }
    public Restaurant Restaurant { get; set; }
    public Courier Courier { get; set; }
    
    [ForeignKey("OrderId")]
    public List<OrderDetail> OrderDetails { get; set; }

}