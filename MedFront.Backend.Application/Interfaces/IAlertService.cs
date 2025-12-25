using MedFront.Backend.Domain.Entities;

namespace MedFront.Backend.Application.Interfaces
{
    public interface IAlertService
    {
        Task CheckAndCreateAlertsAsync(MedicationStock stock, CancellationToken ct);
    }
}
