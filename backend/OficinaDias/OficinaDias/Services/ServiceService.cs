using Microsoft.EntityFrameworkCore;
using OficinaDias.Data;
using OficinaDias.Models;

namespace OficinaDias.Services;

public class ServiceService
{
    private readonly OficinaDbContext _context;

    public ServiceService(OficinaDbContext context)
    {
        _context = context;
    }

    public async Task<Service> CreateAsync(Service service)
    {
        ValidateService(service);

        _context.Services.Add(service);

        await _context.SaveChangesAsync();

        return service;
    }

    public async Task<Service> UpdateAsync(int id, Service updatedService)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service is null)
            throw new Exception(
                "Serviço não encontrado.");

        ValidateService(updatedService);

        service.Description = updatedService.Description;
        service.SuggestedPrice = updatedService.SuggestedPrice;

        await _context.SaveChangesAsync();

        return service;
    }

    public async Task<Service> DeleteAsync(int id)
    {
        var service = await _context.Services
            .FirstOrDefaultAsync(s => s.Id == id);

        if (service is null)
            throw new Exception(
                "Serviço não encontrado.");

        _context.Services.Remove(service);

        await _context.SaveChangesAsync();

        return service;
    }

    private async void ValidateService(Service service)
    {
        if (string.IsNullOrWhiteSpace(service.Description))
            throw new Exception(
                "Descrição é obrigatória.");

        if (service.SuggestedPrice <= 0)
            throw new Exception(
                "Preço deve ser maior que zero.");
    }
}
