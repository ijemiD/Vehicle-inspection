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
    public class InspectionService : IInspectionService
    {
        private readonly VehicleInspectionContext _dbContext;

        public InspectionService(VehicleInspectionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<InspectionDto>> GetInspectionsByVehicleAsync(Guid vehicleId)
        {
            return await _dbContext.Inspections
                            .AsNoTracking()
                            .Where(i => i.VehicleId == vehicleId)
                            .Select(i => MapToDto(i))
                            .ToListAsync();
        }

        public async Task<List<InspectionDto>> GetInspectionsByVehicleAsync(string vin)
        {
            return await _dbContext.Inspections
                            .AsNoTracking()
                            .Where(i => String.Equals(i.Vehicle.Vin, vin, StringComparison.OrdinalIgnoreCase))
                            .Include(i => i.Vehicle)
                            .Select(i => MapToDto(i))
                            .ToListAsync();
        }

        public InspectionDto GetInspectionAsync(Guid id)
        {
            var inspection = _dbContext.Inspections
                                .AsNoTracking()
                                .SingleOrDefault(i => i.Id == id);

            if (inspection == null) return null;

            return MapToDto(inspection);
        }

        public async Task<InspectionDto> UpdateVehicleInspection(InspectionDto updateDto)
        {
            var inspection = _dbContext.Inspections
                                .SingleOrDefault(i => i.Id == updateDto.Id);

            if (inspection == null)
            {
                throw new ArgumentException($"Vehicle inspection entry could not exists!");
            }

            inspection.UpdateResult(updateDto.DidTestPass);
            inspection.UpdateInspector(updateDto.InspectorName);
            inspection.UpdateLocation(updateDto.InspectionLocation);
            inspection.UpdateNotes(updateDto.InspectionNotes);
            inspection.SetDate(updateDto.InspectionDate);

            await _dbContext.SaveChangesAsync();

            return MapToDto(inspection);
        }

        public void DeleteInspection(Guid id)
        {
            var inspection = _dbContext.Inspections.SingleOrDefault(i => i.Id == id);
            inspection.Delete();
            _dbContext.SaveChanges();
        }

        public async Task<InspectionDto> CreateInspectionAsync(InspectionDto createDto)
        {
            var vehicle = _dbContext.Vehicles.SingleOrDefault(v => v.Id == createDto.VehicleId);
            if (vehicle == null)
            {
                throw new ArgumentException("A valid vehicle entry is required!");
            }
            var inspection = new
                Inspection(createDto.InspectorName, createDto.InspectionLocation, createDto.InspectionDate, vehicle);

            _dbContext.Inspections.Add(inspection);

            await _dbContext.SaveChangesAsync();
            return MapToDto(inspection);
        }

        private InspectionDto MapToDto(Inspection inspection)
        {
            return new InspectionDto
            {
                Id = inspection.Id,
                InspectorName = inspection.Inspector,
                InspectionDate = inspection.Date,
                InspectionLocation = inspection.Location,
                InspectionNotes = inspection.Notes,
                DidTestPass = inspection.Passed,
                VehicleId = inspection.Vehicle.Id
            };
        }
    }
}