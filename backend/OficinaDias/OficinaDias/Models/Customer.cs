namespace OficinaDias.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public List<Vehicle> Vehicles { get; set; } = [];
    public List<ServiceOrder> ServiceOrders { get; set; } = [];
}
