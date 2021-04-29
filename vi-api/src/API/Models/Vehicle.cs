using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using VehicleInspection.API.Utilities;

namespace VehicleInspection.API.Models
{
    public class Vehicle
    {
        public Guid Id { get; private set; }
        public string Vin { get; private set; }
        public VehicleMake Make { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public bool Deleted { get; private set; } // for soft delete

        public Vehicle() { }
        public Vehicle(string vin, int year, VehicleMake make, string model)
        {
            if (make == VehicleMake.Null)
            {
                throw new ArgumentException("Vehicle make is required!", nameof(make));
            }
            this.Vin = vin;
            this.Year = year;
            this.Make = make;
            this.Model = model;
            this.Deleted = false;
            this.Id = Guid.NewGuid();
        }

        public void SetYear(int year)
        {
            this.Year = year;
        }

        public void SetMaker(VehicleMake make)
        {
            this.Make = make;
        }

        public void SetModel(string model)
        {
            this.Model = model;
        }

        public void SetVin(string vin)
        {
            this.Vin = vin;
        }

        public void Delete()
        {
            this.Deleted = true;
        }
    }
}