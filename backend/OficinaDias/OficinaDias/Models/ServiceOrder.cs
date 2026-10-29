namespace OficinaDias.Models;

public class ServiceOrder
{
    public int Id { get; set; }
    public DateTime OpenDate { get; set; }
    public DateTime? CloseDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool WasPaid { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;
    public List<ServiceOrderItem> Items { get; set; } = [];

    public decimal Total =>
        Items.Sum(x => x.Quantity * x.UnitPrice);
}
