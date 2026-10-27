namespace OficinaDias.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantitdade inválida.");

        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantidade inválida.");

        if (StockQuantity < quantity)
            throw new InvalidOperationException("Estoque insuficiente.");

        StockQuantity -= quantity;
    }
}
