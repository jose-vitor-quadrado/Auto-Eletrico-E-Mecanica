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

    public async Task<Vehicle> CreateAsync(Vehicle vehicle)
    {
        await ValidateVehicleAsync(vehicle);

        _context.Vehicles.Add(vehicle);

        await _context.SaveChangesAsync();

        return vehicle;
    }

    public async Task<Vehicle> UpdateAsync(
        int id,
        Vehicle updatedVehicle)
    {
        var vehicle = await _context.Vehicles
            .FindAsync(id);

        if (vehicle is null)
            throw new Exception(
                "Veículo não encontrado.");

        await ValidateVehicleAsync(updatedVehicle);

        vehicle.Brand = updatedVehicle.Brand;
        vehicle.Model = updatedVehicle.Model; 
        vehicle.Color = updatedVehicle.Color;
        vehicle.MileageInKm = updatedVehicle.MileageInKm; 
        vehicle.LicensePlate = updatedVehicle.LicensePlate;

        await _context.SaveChangesAsync();

        return vehicle;
    }

    public async Task<Vehicle> DeleteAsync(int id)
    {
        var vehicle = await _context.Vehicles
            .FindAsync(id);

        if (vehicle is null)
            throw new Exception(
                "Veículo não encontrado.");

        _context.Vehicles.Remove(vehicle);

        await _context.SaveChangesAsync();

        return vehicle;
    }

    private async Task ValidateVehicleAsync(
        Vehicle vehicle,
        int? updatingVehicleId = null)
    {
        if (string.IsNullOrWhiteSpace(vehicle.LicensePlate))
            throw new Exception(
                "Placa obrigatória.");

        bool plateExists = await _context.Vehicles
            .AnyAsync(v =>
            v.LicensePlate == vehicle.LicensePlate &&
            v.Id != updatingVehicleId);

        if (plateExists)
            throw new Exception(
                "Já existe um veículo com esta placa.");

        if (string.IsNullOrWhiteSpace(vehicle.Brand))
            throw new Exception(
                "Marca do veículo é obrigatória.");

        if (string.IsNullOrWhiteSpace(vehicle.Model))
            throw new Exception(
                "Modelo do veículo é obrigatória.");

        if (string.IsNullOrWhiteSpace(vehicle.Color))
            throw new Exception(
                "Cor do veículo é obrigatória.");

        if (vehicle.MileageInKm < 0)
            throw new Exception(
                "Quilometragem inválida");
    }
}
