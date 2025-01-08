using Domain.Enums;

namespace Domain.DTOs;

public record OrderDTO
{
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
}

public record CreateOrderDTO : OrderDTO;

public record UpdateOrderDTO : OrderDTO
{
    public int Id { get; set; }
}

public record ReadOrderDTO : UpdateOrderDTO
{
    public List<UpdateOrderDetailDTO> OrderDetails { get; set; }
}

public record GetCountOrdersInEveryoneStatus
{
    public string Status { get; set; }
    public int Count { get; set; }
}

public record GetUsersAndOrdersCount
{
    public UpdateUserDTO User { get; set; }
    public int OrderCount { get; set; }
}