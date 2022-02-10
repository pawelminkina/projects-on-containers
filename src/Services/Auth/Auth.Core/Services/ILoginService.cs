using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Core.Services
{
    public interface ILoginService<TUser>
    {
        Task<TUser> FindByUsernameAsync(string username);

        Task<bool> ValidateCredentialsAsync(TUser user, string password);
    }
}
