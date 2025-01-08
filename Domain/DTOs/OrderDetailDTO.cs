namespace Domain.DTOs;

public record OrderDetailDTO
{
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string SpecialInstructions { get; set; }
}

public record CreateOrderDetailDTO : OrderDetailDTO;

public record UpdateOrderDetailDTO : OrderDetailDTO
{
    public int Id { get; set; }
}

public record ReadOrderDetailDTO : UpdateOrderDetailDTO
{
    public UpdateOrderDTO Order { get; set; }
}