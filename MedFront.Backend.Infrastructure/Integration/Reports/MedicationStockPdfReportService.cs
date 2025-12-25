using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MedFront.Backend.Infrastructure.Integration.Reports
{
    public class MedicationStockPdfReportService : IMedicationStockPdfReportService
    {
        private readonly IMedFrontDbContext _context;

        public MedicationStockPdfReportService(IMedFrontDbContext context)
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
                .Select(x => new MedicationStockReportRowDto
                {
                    MedicationName = x.Medication.Name,
                    WarehouseAddress = x.Warehouse.Address,
                    BoxQuantity = x.BoxQuantity,
                    StockNorm = x.StockNorm,
                    ExpirationDate = x.ExpirationDate.HasValue
                        ? x.ExpirationDate.Value.ToString("yyyy-MM-dd")
                        : null
                })
                .ToListAsync(ct);

            var doc = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("Medication Stock Report").FontSize(18).SemiBold();
                        col.Item().Text($"Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC")
                            .FontSize(9).FontColor(Colors.Grey.Darken2);
                        col.Item().LineHorizontal(1);
                    });

                    page.Content().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3);
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(1);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCell).Text("Medication");
                            header.Cell().Element(HeaderCell).Text("Warehouse");
                            header.Cell().Element(HeaderCell).AlignRight().Text("Qty");
                            header.Cell().Element(HeaderCell).AlignRight().Text("Norm");
                            header.Cell().Element(HeaderCell).Text("Expiration");

                            static IContainer HeaderCell(IContainer c) =>
                                c.PaddingVertical(6).PaddingHorizontal(4)
                                 .Background(Colors.Grey.Lighten3)
                                 .DefaultTextStyle(x => x.SemiBold());
                        });

                        foreach (var r in rows)
                        {
                            table.Cell().Element(BodyCell).Text(r.MedicationName);
                            table.Cell().Element(BodyCell).Text(r.WarehouseAddress);
                            table.Cell().Element(BodyCell).AlignRight().Text(r.BoxQuantity.ToString());
                            table.Cell().Element(BodyCell).AlignRight().Text(r.StockNorm.ToString());
                            table.Cell().Element(BodyCell).Text(r.ExpirationDate ?? "-");

                            static IContainer BodyCell(IContainer c) =>
                                c.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                                 .PaddingVertical(4).PaddingHorizontal(4);
                        }
                    });

                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                        x.Span(" / ");
                        x.TotalPages();
                    });
                });
            });

            return doc.GeneratePdf();
        }
    }
}
