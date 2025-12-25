using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedFront.Backend.Application.Interfaces
{
    public interface IMedicationStockPdfReportService
    {
        Task<byte[]> BuildAsync(CancellationToken ct);
    }
}

