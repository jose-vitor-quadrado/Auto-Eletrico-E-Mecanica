namespace OficinaDias.Models;

public class ServiceOrderItem
{
    public int Id { get; set; }
    public string ItemDescription { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public int ServiceOrderId { get; set; }
    public ServiceOrder ServiceOrder { get; set; } = null!;
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    public int? ServiceId { get; set; }
    public Service? Service { get; set; }

    public decimal Total => Quantity * UnitPrice;
}
