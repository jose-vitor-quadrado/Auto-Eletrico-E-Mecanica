using Microsoft.EntityFrameworkCore;
using OficinaDias.Data;
using OficinaDias.Models;

namespace OficinaDias.Services;

public class ServiceOrderService
{
    private readonly OficinaDbContext _context;

    public ServiceOrderService(OficinaDbContext context)
    {
        _context = context;
    }

    public async Task ValidateAsync(ServiceOrder order)
    {
        if (order.Items.Count == 0)
            throw new Exception(
                "A ordem deve possuir ao menos um item.");

        foreach(var item in order.Items)
        {
            var hasProduct = item.ProductId.HasValue;
            var hasService = item.ServiceId.HasValue;

            if (!hasProduct && !hasService)
                throw new Exception(
                    "O item deve possuir um produto ou serviço.");

            if (hasProduct && hasService)
                throw new Exception(
                    "O item não deve possuir produto e serviço ao mesmo tempo.");

            if (item.Quantity <= 0)
                throw new Exception(
                    "A quantidade deve ser maior que zero.");

            if (item.UnitPrice <= 0)
                throw new Exception(
                    "O valor não pode ser negativo.");

            if (hasProduct)
            {
                var product = await _context.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product is null)
                    throw new Exception("Produto não encontrado.");
            }

            if (hasService)
            {
                var service = await _context.Services
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.Id == item.ServiceId);

                if (service is null)
                    throw new Exception("Serviço não encontrado.");
            }
        }
    }
}
