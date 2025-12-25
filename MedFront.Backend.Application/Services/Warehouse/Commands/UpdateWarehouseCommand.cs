using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.UpdateDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Warehouse.Commands
{
    public class UpdateWarehouseCommand : IRequest
    {
        public Guid Id { get; }
        public WarehouseUpdateDto Dto { get; }

        public UpdateWarehouseCommand(Guid id, WarehouseUpdateDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public class UpdateWarehouseCommandHandler : IRequestHandler<UpdateWarehouseCommand>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public UpdateWarehouseCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Warehouses
                .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

            if (entity is null)
                throw new KeyNotFoundException("Warehouse not found");

            _mapper.Map(request.Dto, entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
