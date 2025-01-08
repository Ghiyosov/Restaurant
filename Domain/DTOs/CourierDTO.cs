using Domain.Enums;

namespace Domain.DTOs;

public record CourierDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public CourierStatus Status { get; set; }
    public string CurrentLocation { get; set; }
    public decimal Rating { get; set; }
    public TransportType TransportType { get; set; }
}

public record CreateCourierDTO : CourierDTO;

public record UpdateCourierDTO : CourierDTO
{
    public int Id { get; set; }
}

public record ReadCourierDTO : UpdateCourierDTO
{
    public UpdateUserDTO User { get; set; }
    public List<UpdateOrderDTO> Orders { get; set; }
}