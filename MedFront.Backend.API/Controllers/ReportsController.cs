using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Infrastructure.Integration.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedFront.Backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly IMedicationStockPdfReportService _report;
        private readonly IMedicationStockCsvReportService _csvReport;

        public ReportsController(
            IMedicationStockPdfReportService report,
            IMedicationStockCsvReportService csvReport)
        {
            _report = report;
            _csvReport = csvReport;
        }

        [HttpGet("medication-stock")]
        public async Task<IActionResult> MedicationStock(CancellationToken ct)
        {
            var pdf = await _report.BuildAsync(ct);
            return File(
                pdf,
                "application/pdf",
                $"medication-stock-{DateTime.UtcNow:yyyyMMdd-HHmm}.pdf");
        }

        [HttpGet("medication-stock/csv")]
        public async Task<IActionResult> MedicationStockCsv(CancellationToken ct)
        {
            var csv = await _csvReport.BuildAsync(ct);
            return File(
                csv,
                "text/csv",
                $"medication-stock-{DateTime.UtcNow:yyyyMMdd-HHmm}.csv");
        }
    }
}
