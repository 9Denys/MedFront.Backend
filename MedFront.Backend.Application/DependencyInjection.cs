using FluentValidation;
using MedFront.Backend.Application.Common.Behavior;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Application.Services.Warehouse;
using MedFront.Backend.Application.Services.WarehouseAccess;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MedFront.Backend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

            services.AddScoped<IWarehouseAccessService, WarehouseAccessService>();

            services.AddScoped<WarehouseOccupancyService>();

            


            return services;
        }
    }
}
