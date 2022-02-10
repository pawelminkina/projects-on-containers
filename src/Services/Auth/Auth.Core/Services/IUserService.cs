using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Services
{
    public interface IUserService<TUser>
    {
        Task<TUser> GetUserByIdAsync(string id);
    }
}
