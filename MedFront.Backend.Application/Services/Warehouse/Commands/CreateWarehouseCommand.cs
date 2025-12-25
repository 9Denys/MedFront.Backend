using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MediatR;

namespace MedFront.Backend.Application.Services.Warehouse.Commands
{
    public class CreateWarehouseCommand : IRequest<Guid>
    {
        public WarehouseCreateDto Dto { get; }

        public CreateWarehouseCommand(WarehouseCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateWarehouseCommandHandler : IRequestHandler<CreateWarehouseCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public CreateWarehouseCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Warehouse>(request.Dto);

            entity.CurrentOccupancy = 0;

            _context.Warehouses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
