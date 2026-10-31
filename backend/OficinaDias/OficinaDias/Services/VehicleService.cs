using Microsoft.EntityFrameworkCore;
using OficinaDias.Data;
using OficinaDias.Models;

namespace OficinaDias.Services;

public class VehicleService
{
    private readonly OficinaDbContext _context;

    public VehicleService(OficinaDbContext context)
    {
        _context = context;
    }
}
