using System.Text;
using MedFront.Backend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Infrastructure.Integration.Reports
{
    public interface IMedicationStockCsvReportService
    {
        Task<byte[]> BuildAsync(CancellationToken ct);
    }

    public class MedicationStockCsvReportService : IMedicationStockCsvReportService
    {
        private readonly IMedFrontDbContext _context;

        public MedicationStockCsvReportService(IMedFrontDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]> BuildAsync(CancellationToken ct)
        {
            var rows = await _context.MedicationStocks
                .AsNoTracking()
                .Include(x => x.Medication)
                .Include(x => x.Warehouse)
                .OrderBy(x => x.Medication.Name)
                .ThenBy(x => x.Warehouse.Address)
                .Select(x => new
                {
                    MedicationName = x.Medication.Name,
                    WarehouseAddress = x.Warehouse.Address,
                    x.BoxQuantity,
                    x.StockNorm,
                    ExpirationDate = x.ExpirationDate.HasValue
                        ? x.ExpirationDate.Value.ToString("yyyy-MM-dd")
                        : ""
                })
                .ToListAsync(ct);

            var sb = new StringBuilder();

            sb.AppendLine("MedicationName,WarehouseAddress,BoxQuantity,StockNorm,ExpirationDate");

            foreach (var r in rows)
            {
                sb.Append(Escape(r.MedicationName)).Append(',')
                  .Append(Escape(r.WarehouseAddress)).Append(',')
                  .Append(r.BoxQuantity).Append(',')
                  .Append(r.StockNorm).Append(',')
                  .Append(Escape(r.ExpirationDate))
                  .AppendLine();
            }

            var utf8Bom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);
            return utf8Bom.GetBytes(sb.ToString());
        }

        private static string Escape(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            var mustQuote = value.Contains(',') || value.Contains('"') || value.Contains('\n') || value.Contains('\r');
            if (!mustQuote)
                return value;

            return $"\"{value.Replace("\"", "\"\"")}\"";
        }
    }
}
