namespace OficinaDias.Models;

public class ServiceOrderItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public decimal Total => Quantity * UnitPrice;
}
