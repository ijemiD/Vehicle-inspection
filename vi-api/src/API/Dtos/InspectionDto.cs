
namespace VehicleInspection.API.Dtos
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class InspectionDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        [Required]
        [Display(Name = "Inspector's Name")]
        public string InspectorName { get; set; }
        [Required]
        [Display(Name = "Inspection Location")]
        public string InspectionLocation { get; set; }
        [Required]
        [Display(Name = "Inspection Date")]
        [DataType(DataType.Date)]
        public DateTime InspectionDate { get; set; }
        [Display(Name = "Inspection Note(s)")]
        public string InspectionNotes { get; set; }
        [Display(Name = "Overall Test Pass/Fail")]
        public bool DidTestPass { get; set; } = false;
        [Required]
        public Guid VehicleId { get; set; }
    }
}