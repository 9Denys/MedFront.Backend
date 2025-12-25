using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Infrastructure.Integration.Alerts
{
    public class AlertService : IAlertService
    {
        private readonly MedFrontDbContext _context;
        private const int ExpiringSoonDays = 30;

        public AlertService(MedFrontDbContext context)
        {
            _context = context;
        }

        public async Task CheckAndCreateAlertsAsync(MedicationStock stock, CancellationToken ct)
        {
            
            if (stock.Medication is null)
            {
                stock.Medication = await _context.Medications
                    .AsNoTracking()
                    .FirstAsync(m => m.Id == stock.MedicationId, ct);
            }

            var alerts = new List<Alert>();

            if (stock.BoxQuantity <= stock.StockNorm)
            {
                alerts.Add(new Alert
                {
                    WarehouseId = stock.WarehouseId,
                    MedicationId = stock.MedicationId,
                    Message = $"Critical stock level: {stock.Medication.Name}. Remaining {stock.BoxQuantity} boxes, norm {stock.StockNorm}.",
                    IsRead = false,
                    ReadAt = null
                });
            }

            if (stock.ExpirationDate is not null)
            {
                var today = DateOnly.FromDateTime(DateTime.UtcNow);
                var exp = stock.ExpirationDate.Value;

                if (exp <= today)
                {
                    alerts.Add(new Alert
                    {
                        WarehouseId = stock.WarehouseId,
                        MedicationId = stock.MedicationId,
                        Message = $"Expiration date has passed: {stock.Medication.Name}. Date: {exp:yyyy-MM-dd}.",
                        IsRead = false,
                        ReadAt = null
                    });
                }
                else if (exp <= today.AddDays(ExpiringSoonDays))
                {
                    alerts.Add(new Alert
                    {
                        WarehouseId = stock.WarehouseId,
                        MedicationId = stock.MedicationId,
                        Message = $"Expiration date is approaching: {stock.Medication.Name}.Date: {exp:yyyy-MM-dd}.",
                        IsRead = false,
                        ReadAt = null
                    });
                }
            }

            if (alerts.Count > 0)
            {
                await _context.Alerts.AddRangeAsync(alerts, ct);
            }
        }
    }
}
