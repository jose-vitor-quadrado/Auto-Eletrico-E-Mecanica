using Microsoft.EntityFrameworkCore;
using OficinaDias.Data;
using OficinaDias.Models;

namespace OficinaDias.Services;

public class ProductService
{
    private readonly OficinaDbContext _context;

    public ProductService(OficinaDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        ValidateProduct(product);

        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        ValidateProduct(product);

        _context.Products.Update(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> DeleteAsync(int id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
            throw new Exception(
                "Produto não encontrado.");

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> IncreaseStockAsync(int productId, int quantity)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null)
            throw new Exception(
                "Produto não encontrado.");

        product.IncreaseStock(quantity);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product> DecreaseStockAsync(int productId, int quantity)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId);

        if (product is null)
            throw new Exception(
                "Produto não encontrado.");

        product.DecreaseStock(quantity);

        await _context.SaveChangesAsync();

        return product;
    }

    private async void ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new Exception(
                "Nome é obrigatório.");

        if (product.Price <= 0)
            throw new Exception(
                "Preço deve ser maior que zero.");

        if (product.StockQuantity < 0)
            throw new Exception(
                "Estoque inválido.");
    }
}
