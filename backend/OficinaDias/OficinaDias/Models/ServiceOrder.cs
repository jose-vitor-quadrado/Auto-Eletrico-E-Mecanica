namespace OficinaDias.Models;

public class ServiceOrder
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = new();
    public DateTime OpenDate { get; set; } = DateTime.Now;
    public DateTime? CloseDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool WasPaid { get; set; }
    public List<ServiceOrderItem> Items { get; set; } = [];

    public decimal Total =>
        Items.Sum(x => x.Quantity * x.UnitPrice);
}
