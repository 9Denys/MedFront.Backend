using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.CreateDTOs;
using MedFront.Backend.Domain.Entities;
using MedFront.Backend.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Request.Commands
{
    public class CreateRequestCommand : IRequest<Guid>
    {
        public CreateRequestDto Dto { get; }

        public CreateRequestCommand(CreateRequestDto dto)
        {
            Dto = dto;
        }
    }

    public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IUserContextService _userContext;
        private readonly IWarehouseAccessService _access;
        private readonly IMapper _mapper;

        public CreateRequestCommandHandler(
            IMedFrontDbContext context,
            IUserContextService userContext,
            IWarehouseAccessService access,
            IMapper mapper)
        {
            _context = context;
            _userContext = userContext;
            _access = access;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateRequestCommand request, CancellationToken ct)
        {
            if (!_userContext.IsAuthenticated())
                throw new UnauthorizedAccessException("User is not authenticated.");

            var userId = _userContext.GetCurrentUserId();
            if (userId is null)
                throw new UnauthorizedAccessException("UserId is not found in token.");

            await _access.EnsureAccessAsync(request.Dto.WarehouseId, ct);

            var warehouseExists = await _context.Warehouses
                .AsNoTracking()
                .AnyAsync(w => w.Id == request.Dto.WarehouseId, ct);
            if (!warehouseExists)
                throw new Exception("Warehouse not found.");

            var medicationExists = await _context.Medications
                .AsNoTracking()
                .AnyAsync(m => m.Id == request.Dto.MedicationId, ct);
            if (!medicationExists)
                throw new Exception("Medication not found.");

            if (request.Dto.BoxQuantity <= 0)
                throw new Exception("BoxQuantity must be > 0.");

            var entity = _mapper.Map<Domain.Entities.Request>(request.Dto);
            entity.UserId = userId.Value;
            entity.RequestStatus = RequestStatus.Created;

            _context.Requests.Add(entity);
            await _context.SaveChangesAsync(ct);

            return entity.Id;
        }
    }
}
