using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Mvc;


namespace ReactVeloShop.Server.Controllers.Api
{
    [ApiController]
    [Route("/api/home")]
    public class HomeApiController : ControllerBase
    {
        private IUserService _userService;

        public HomeApiController(IUserService userService)
        {
            _userService = userService;
        }
    }
}
