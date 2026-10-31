using Microsoft.EntityFrameworkCore;
using OficinaDias.Data;
using OficinaDias.Models;

namespace OficinaDias.Services;

public class CustomerService
{
    private readonly OficinaDbContext _context;

    public CustomerService(OficinaDbContext context)
    {
        _context = context;
    }

    private static void ValidateCustomer(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.Name))
            throw new Exception(
                "Nome obrigatório.");

        if (string.IsNullOrWhiteSpace(customer.Phone))
            throw new Exception(
                "Telefone obrigatório.");

        if (string.IsNullOrWhiteSpace(customer.Neighborhood))
            throw new Exception(
                "Bairro obrigatório.");

        if (string.IsNullOrWhiteSpace(customer.Street))
            throw new Exception(
                "Endereço obrigatório.");
    }
}
