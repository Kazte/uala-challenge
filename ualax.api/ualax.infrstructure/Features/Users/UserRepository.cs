using Microsoft.EntityFrameworkCore;
using ualax.application.Abstractions.Database;
using ualax.domain.Features.User;

namespace ualax.infrastructure.Features.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Add(UserEntity user)
        {
            var e = _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return e.Entity;
        }

        public async Task<UserEntity?> GetByUsername(string username)
        {
            // AsNoTracking ->  Esto mejora el rendimiento a la hora de 
            //                  hacer una llamada de solo lectura.
            return await _context.Users.AsNoTracking().SingleOrDefaultAsync(x => x.Username.Equals(username));
        }
    }
}
