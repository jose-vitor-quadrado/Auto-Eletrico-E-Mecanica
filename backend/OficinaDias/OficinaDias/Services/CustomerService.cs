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

    public async Task<Customer> CreateAsync(Customer customer)
    {
        ValidateCustomer(customer);

        _context.Customers.Add(customer);

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> UpdateAsync(
        int id, 
        Customer updatedCustomer)
    {
        var customer = await _context.Customers
            .FindAsync(id);

        if (customer is null)
            throw new Exception(
                "Cliente não encontrado.");

        ValidateCustomer(updatedCustomer);

        customer.Name = updatedCustomer.Name;
        customer.Phone = updatedCustomer.Phone;
        customer.Neighborhood = updatedCustomer.Neighborhood;
        customer.Street = updatedCustomer.Street;

        await _context.SaveChangesAsync();

        return customer;
    }

    public async Task<Customer> DeleteAsync(int id)
    {
        var customer = await _context.Customers
            .FindAsync(id);

        if (customer is null)
            throw new Exception(
                "Cliente não encontrado.");

        _context.Customers.Remove(customer);

        await _context.SaveChangesAsync();

        return customer;
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
