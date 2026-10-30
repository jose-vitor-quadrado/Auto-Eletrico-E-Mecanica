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
}
