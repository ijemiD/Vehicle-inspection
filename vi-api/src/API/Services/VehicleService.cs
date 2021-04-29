using System;
using System.Threading.Tasks;
using VehicleInspection.API.Dtos;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VehicleInspection.API.Infrastructure;
using VehicleInspection.API.Models;

namespace VehicleInspection.API.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly VehicleInspectionContext _dbContext;
        public VehicleService(VehicleInspectionContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<VehicleDto>> GetVehicles()
        {
            return await _dbContext.Vehicles
                            .AsNoTracking()
                        .Select(v => MapToDto(v))
                        .ToListAsync();
        }

        public VehicleDto GetVehicle(Guid id)
        {
            var vehicle = _dbContext.Vehicles
                            .AsNoTracking()
                        .SingleOrDefault(v => v.Id == id);

            if (vehicle == null) return null;

            return MapToDto(vehicle);
        }

        public VehicleDto GetVehicle(string vin)
        {
            var vehicle = _dbContext.Vehicles
                            .AsNoTracking()
                        .SingleOrDefault(v => String.Equals(v.Vin, vin, StringComparison.OrdinalIgnoreCase));

            if (vehicle == null) return null;

            return MapToDto(vehicle);
        }

        public async Task<VehicleDto> CreateVehicleAsync(VehicleDto dto)
        {
            var vehicleMake = ValidateMake(dto.VehicleMake);
            if (_dbContext.Vehicles.Any(vehicle => String.Equals(vehicle.Vin, dto.Vin, StringComparison.OrdinalIgnoreCase) &&
                                        vehicle.Year == dto.VehicleYear &&
                                        vehicle.Model == dto.VehicleModel &&
                                        vehicle.Make == vehicleMake))
            {
                throw new ArgumentException($"Vehicle entry {dto.Vin} already exists!");
            }

            var vehicle = new Vehicle(dto.Vin, dto.VehicleYear, vehicleMake, dto.VehicleModel);

            _dbContext.Vehicles.Add(vehicle);
            await _dbContext.SaveChangesAsync();

            return MapToDto(vehicle);
        }

        public async Task<VehicleDto> UpdateVehicle(VehicleDto dto)
        {
            var vehicleMake = ValidateMake(dto.VehicleMake);
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.Id == dto.Id);
            if (vehicle == null)
            {
                throw new ArgumentException($"Vehicle entry {dto.Vin} does not exists!");
            }
            vehicle.SetVin(dto.Vin);
            vehicle.SetYear(dto.VehicleYear);
            vehicle.SetMaker(vehicleMake);
            vehicle.SetModel(dto.VehicleModel);

            await _dbContext.SaveChangesAsync();
            return MapToDto(vehicle);
        }

        public async Task DeleteVehicleAsync(Guid id)
        {
            // Get inspections and delete
            var inspections = _dbContext.Inspections.Where(i => i.VehicleId == id);
            foreach (var inspection in inspections)
            {
                inspection.Delete();
            }
            var vehicle = _dbContext.Vehicles.FirstOrDefault(v => v.Id == id);
            vehicle.Delete();
            await _dbContext.SaveChangesAsync();
        }

        private static VehicleDto MapToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                Vin = vehicle.Vin,
                VehicleMake = vehicle.Make.Name,
                VehicleYear = vehicle.Year,
                VehicleModel = vehicle.Model
            };
        }

        private VehicleMake ValidateMake(string make)
        {
            var vehicleMake = VehicleMake.FromString(make);
            if (vehicleMake == VehicleMake.Null)
            {
                throw new ArgumentException("Vehicle make is required!");
            }
            return vehicleMake;
        }
    }
}