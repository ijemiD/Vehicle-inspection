using System;
using System.Threading.Tasks;
using VehicleInspection.API.Dtos;
using System.Collections.Generic;
using VehicleInspection.API.Infrastructure;

namespace VehicleInspection.API.Services
{
    public interface IInspectionService
    {
        Task<List<InspectionDto>> GetInspectionsByVehicleAsync(Guid vehicleId);
        Task<List<InspectionDto>> GetInspectionsByVehicleAsync(string vin);
        InspectionDto GetInspectionAsync(Guid id);
        Task<InspectionDto> UpdateVehicleInspection(InspectionDto updateDto);
        Task<InspectionDto> CreateInspectionAsync(InspectionDto createDto);
        void DeleteInspection(Guid id);
    }
}