using System.Security.AccessControl;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace VehicleInspection.API.Models
{
    public class VehicleMake
    {
        public static VehicleMake Null { get; } = new VehicleMake(-1, string.Empty);
        public static VehicleMake Ford { get; } = new VehicleMake(0, nameof(Ford));
        public static VehicleMake Tesla { get; } = new VehicleMake(1, nameof(Tesla));
        public static VehicleMake Nissan { get; } = new VehicleMake(2, nameof(Nissan));
        public static VehicleMake Honda { get; } = new VehicleMake(3, nameof(Honda));
        public static VehicleMake Mazda { get; } = new VehicleMake(4, nameof(Mazda));
        public static VehicleMake Toyota { get; } = new VehicleMake(4, nameof(Toyota));
        public static VehicleMake Mercedes { get; } = new VehicleMake(4, nameof(Mercedes));
        public static VehicleMake BMW { get; } = new VehicleMake(4, nameof(BMW));

        public string Name { get; private set; }
        public int Value { get; private set; }

        public static IEnumerable<VehicleMake> List() => new[] { Ford, Tesla, Nissan, Honda, Toyota };

        public static VehicleMake FromString(string makeString)
        {
            var make = List().SingleOrDefault(m => String.Equals(m.Name, makeString, StringComparison.OrdinalIgnoreCase));
            return make == null ? VehicleMake.Null : make;
        }

        public static VehicleMake FromValue(int value)
        {
            return List().Single(m => m.Value == value);
        }

        public VehicleMake(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }
    }
}