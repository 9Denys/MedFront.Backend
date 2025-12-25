using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Infrastructure.Integration.Alerts;
using MedFront.Backend.Infrastructure.Integration.Reports; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedFront.Backend.Infrastructure.Integration.Background;
using MedFront.Backend.Infrastructure.Persistence.Services;

namespace MedFront.Backend.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<MedFrontDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddScoped<IMedFrontDbContext>(provider =>
                provider.GetRequiredService<MedFrontDbContext>());

            services.AddScoped<IAlertService, AlertService>();

            services.AddScoped<IMedicationStockPdfReportService, MedicationStockPdfReportService>();

            services.AddScoped<IMedicationStockCsvReportService, MedicationStockCsvReportService>();

            services.AddScoped<ISensorReadingCleanupService, SensorReadingCleanupService>();
            services.AddHostedService<SensorReadingCleanupWorker>();
            return services;
        }
    }
}
