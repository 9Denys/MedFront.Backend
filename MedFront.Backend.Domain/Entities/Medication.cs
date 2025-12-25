using MedFront.Backend.Domain.Common;

namespace MedFront.Backend.Domain.Entities
{
    public class Medication : BaseEntity
    {
        public required string Name { get; set; }
        public string? Details { get; set; }

        public required string SKU { get; set; }
        public string? Category { get; set; }

        public decimal PackageVolume { get; set; }

        public virtual ICollection<MedicationStock> MedicationStocks { get; set; } = new List<MedicationStock>();
        public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();

        public virtual ICollection<Request> Requests { get; set; } = new List<Request>();



    }
}
