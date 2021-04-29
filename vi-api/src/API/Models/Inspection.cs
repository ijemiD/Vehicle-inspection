using System.ComponentModel.DataAnnotations;
using System;

namespace VehicleInspection.API.Models
{
    public class Inspection
    {
        public Guid Id { get; private set; }
        public bool Passed { get; private set; } = false;
        public string Inspector { get; private set; }
        public string Location { get; set; }
        public DateTime Date { get; private set; }
        public string Notes { get; private set; }
        public Guid VehicleId { get; private set; }
        public bool Deleted { get; private set; } // for soft delete

        public Vehicle Vehicle { get; private set; }

        public Inspection() { }

        public Inspection(string inspector, string location, DateTime date, Vehicle vehicle)
        {
            this.Id = Guid.NewGuid();
            this.Inspector = inspector;
            this.Vehicle = vehicle;
            this.VehicleId = vehicle.Id;
            this.Passed = false;
            this.Date = DateTime.Now;
        }

        public void UpdateResult(bool passed = true)
        {
            this.Passed = passed;
        }

        public void UpdateInspector(string inspectorName)
        {
            this.Inspector = inspectorName;
        }

        public void UpdateNotes(string notes)
        {
            this.Notes = notes;
        }

        public void UpdateLocation(string location)
        {
            this.Location = location;
        }

        public void SetDate(DateTime inspectionDate)
        {
            this.Date = inspectionDate;
        }

        public void Delete()
        {
            this.Deleted = true;
        }
    }
}