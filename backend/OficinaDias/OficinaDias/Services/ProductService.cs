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
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }
}
