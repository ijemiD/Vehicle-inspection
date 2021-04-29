using System.Threading.Tasks;
using VehicleInspection.API.Dtos;
using System.Collections.Generic;
using System;

namespace VehicleInspection.API.Services
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetVehicles();
        VehicleDto GetVehicle(Guid id);
        VehicleDto GetVehicle(string vin);
        Task<VehicleDto> UpdateVehicle(VehicleDto dto);
        Task<VehicleDto> CreateVehicleAsync(VehicleDto dto);
        Task DeleteVehicleAsync(Guid id);
    }
}