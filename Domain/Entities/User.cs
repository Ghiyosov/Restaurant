using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    [Phone]
    public string Phone { get; set; }
    [PasswordPropertyText]
    public string Password { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }
    [ForeignKey("UserId")]
    public List<Courier> Couriers { get; set; }
    [ForeignKey("CouriersId")]
    public List<Order> Orders { get; set; }
}