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
