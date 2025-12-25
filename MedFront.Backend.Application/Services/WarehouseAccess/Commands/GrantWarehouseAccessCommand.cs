using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace MedFront.Backend.Application.Services.WarehouseAccess.Commands
{
    public class GrantWarehouseAccessCommand : IRequest<Guid>
    {
        public WarehouseAccessCreateDto Dto { get; }

        public GrantWarehouseAccessCommand(WarehouseAccessCreateDto dto)
        {
            Dto = dto;
        }
    }

    public class GrantWarehouseAccessCommandHandler : IRequestHandler<GrantWarehouseAccessCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public GrantWarehouseAccessCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(GrantWarehouseAccessCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == request.Dto.UserId, cancellationToken);

            if (!userExists)
                throw new KeyNotFoundException("User not found");

            var warehouseExists = await _context.Warehouses
                .AsNoTracking()
                .AnyAsync(w => w.Id == request.Dto.WarehouseId, cancellationToken);

            if (!warehouseExists)
                throw new KeyNotFoundException("Warehouse not found");

            var alreadyExists = await _context.WarehouseAccesses
                .AsNoTracking()
                .AnyAsync(x => x.UserId == request.Dto.UserId && x.WarehouseId == request.Dto.WarehouseId, cancellationToken);

            if (alreadyExists)
                throw new InvalidOperationException("Access already exists");

            var entity = _mapper.Map<Domain.Entities.WarehouseAccess>(request.Dto);

            _context.WarehouseAccesses.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
