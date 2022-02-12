using Microsoft.AspNetCore.Mvc;
using WebBff.Aggregator.Services.User;

namespace WebBff.Aggregator.Controllers
{
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
    }
}
