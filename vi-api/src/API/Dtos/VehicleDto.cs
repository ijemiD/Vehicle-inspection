
namespace VehicleInspection.API.Dtos
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using VehicleInspection.API.Utilities;
    public class VehicleDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        [Display(Name = "Vehicle Identification Number (VIN)")]
        [StringLength(17, MinimumLength = 11, ErrorMessage = "VIN length must be between 11 and 17")]
        [RegularExpression("[A-HJ-NPR-Z0-9]{17}", ErrorMessage = "VIN should match a valid format. E.g. 1HGBH41JXMN109186")]
        public string Vin { get; set; }
        [Required]
        [Display(Name = "Vehicle Maker")]
        public string VehicleMake { get; set; }
        [Required]
        [Display(Name = "Vehicle Year")]
        public int VehicleYear { get; set; }
        [Required]
        [Display(Name = "Vehicle Model")]
        public string VehicleModel { get; set; }
    }
}