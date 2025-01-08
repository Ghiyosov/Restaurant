using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class Courier
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string CurrentLocation { get; set; }
    public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }

    public User User { get; set; }
    [ForeignKey("CourierId")]
    public List<Order> Orders { get; set; }
}