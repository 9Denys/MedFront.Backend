using MedFront.Backend.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedFront.Backend.Infrastructure.Persistence.Services
{
    public class SensorReadingCleanupService : ISensorReadingCleanupService
    {
        private readonly MedFrontDbContext _context;
        private readonly ILogger<SensorReadingCleanupService> _logger;

        public SensorReadingCleanupService(
            MedFrontDbContext context,
            ILogger<SensorReadingCleanupService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task CleanupOldReadingsAsync(TimeSpan maxAge, CancellationToken cancellationToken)
        {
            var threshold = DateTime.UtcNow - maxAge;

            _logger.LogInformation("Starting SensorReading cleanup, threshold (Time): {Threshold}", threshold);

            var oldReadings = await _context.SensorReadings
                .IgnoreQueryFilters()
                .Where(r => r.DeletedAt == null && r.Time < threshold)
                .ToListAsync(cancellationToken);

            if (!oldReadings.Any())
            {
                _logger.LogInformation("No SensorReadings older than {Threshold} found.", threshold);
                return;
            }

            _context.SensorReadings.RemoveRange(oldReadings);

            var affected = await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("SensorReading cleanup completed. Deleted {Count} records.", affected);
        }
    }
}
