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

    public async Task<ServiceOrder> CreateAsync(ServiceOrder serviceOrder)
    {
        serviceOrder.OpenDate = DateTime.Now;

        await ValidateAsync(serviceOrder);

        foreach (var item in serviceOrder.Items)
        {
            if (item.ProductId is not null)
            {
                var product = await _context.Products
                    .FindAsync(item.ProductId);

                if (product is null)
                    throw new Exception("Produto não encontrado.");

                product.DecreaseStock(item.Quantity);
            }
        }

        _context.ServiceOrders.Add(serviceOrder);

        await _context.SaveChangesAsync();

        return serviceOrder;
    }

    public async Task MarkAsPaidAsync(int serviceOrderId)
    {
        var order = await _context.ServiceOrders
            .FirstOrDefaultAsync(x => x.Id == serviceOrderId);

        if (order is null)
            throw new Exception(
                "Ordem de serviço não encontrada.");

        order.WasPaid = true;

        await _context.SaveChangesAsync();
    }

    public async Task CloseAsync(int serviceOrderId)
    {
        var order = await _context.ServiceOrders
            .FirstOrDefaultAsync(x => x.Id == serviceOrderId);

        if (order is null)
            throw new Exception(
                "Ordem de serviço não encontrada.");

        order.CloseDate = DateTime.Now;

        await _context.SaveChangesAsync();
    }

    public async Task CancelAsync(int serviceOrderId)
    {
        var order = await _context.ServiceOrders
            .FirstOrDefaultAsync(x => x.Id == serviceOrderId);

        if (order is null)
            throw new Exception(
                "Ordem de serviço não encontrada.");

        foreach(var item in order.Items)
        {
            if (item.ProductId.HasValue)
            {
                var product = await _context.Products
                    .FirstAsync(p => p.Id == item.ProductId);

                product.IncreaseStock(item.Quantity);
            }
        }

        _context.ServiceOrders.Remove(order);

        await _context.SaveChangesAsync();
    }
}
