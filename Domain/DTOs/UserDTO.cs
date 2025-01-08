using Domain.Enums;

namespace Domain.DTOs;

public record UserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public DateTime RegistrationDate { get; set; }
    public UserRole Role { get; set; }
}

public record CreateUserDTO : UserDTO;

public record UpdateUserDTO : UserDTO
{
    public int Id { get; set; }
}

public record ReadUserDTO : UpdateUserDTO
{
    public List<UpdateUserDTO> Couriers { get; set; }
    public List<UpdateOrderDTO> Orders { get; set; }
}