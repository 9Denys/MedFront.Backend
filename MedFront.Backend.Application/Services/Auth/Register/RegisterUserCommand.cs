using AutoMapper;
using MedFront.Backend.Application.Interfaces;
using MedFront.Backend.Contracts.DTOs.AuthDTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MedFront.Backend.Application.Services.Auth.Register
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public required RegisterDto Register { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IMedFrontDbContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IMedFrontDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Register == null)
                throw new ArgumentNullException(nameof(request.Register), "Register payload is required.");

            if (request.Register.User == null)
                throw new ArgumentNullException(nameof(request.Register.User), "User object is required.");

            var userDto = request.Register.User;

            var userExists = await _context.Users
                .AnyAsync(u => u.Email == userDto.Email, cancellationToken);

            if (userExists)
                throw new Exception("User already exists");

            var user = _mapper.Map<Domain.Entities.User>(userDto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.PasswordHash);
            user.CreatedAt = DateTime.UtcNow;

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
