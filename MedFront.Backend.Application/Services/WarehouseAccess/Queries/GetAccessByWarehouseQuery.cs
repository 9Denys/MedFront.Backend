using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.ReadingDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.WarehouseAccess.Queries
{
    public class GetAccessByWarehouseQuery : IRequest<List<WarehouseAccessDto>>
    {
        public Guid WarehouseId { get; }

        public GetAccessByWarehouseQuery(Guid warehouseId)
        {
            WarehouseId = warehouseId;
        }
    }

    public class GetAccessByWarehouseQueryHandler : IRequestHandler<GetAccessByWarehouseQuery, List<WarehouseAccessDto>>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GetAccessByWarehouseQueryHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WarehouseAccessDto>> Handle(GetAccessByWarehouseQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.WarehouseAccesses
                .AsNoTracking()
                .Where(x => x.WarehouseId == request.WarehouseId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<WarehouseAccessDto>>(list);
        }
    }
}
