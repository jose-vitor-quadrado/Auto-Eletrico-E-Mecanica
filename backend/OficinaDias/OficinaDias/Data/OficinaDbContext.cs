using OficinaDias.Models;
using Microsoft.EntityFrameworkCore;

namespace OficinaDias.Data;

public class OficinaDbContext : DbContext
{
    public OficinaDbContext(DbContextOptions<OficinaDbContext> options) : base(options)
    {
    }
}
