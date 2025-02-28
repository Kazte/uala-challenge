using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ualax.domain.Features.User
{
    public interface IUserRepository
    {
        Task<UserEntity> Add(UserEntity user);
        Task<UserEntity?> GetByUsername(string username);
    }
}
