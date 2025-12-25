using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse.Queries
{
    public class GetWarehouseByIdQuery : IRequest<WarehouseDto>
    {
        public Guid Id { get; }

        public GetWarehouseByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, WarehouseDto>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetWarehouseByIdQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WarehouseDto> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            var warehouse = await _context.Warehouses
                .AsNoTracking()
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (warehouse is null)
                throw new KeyNotFoundException("Warehouse not found");

            return _mapper.Map<WarehouseDto>(warehouse);
        }
    }
}
