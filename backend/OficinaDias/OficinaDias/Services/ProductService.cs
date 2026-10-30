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

    public async void ValidateProduct(Product product)
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
}
