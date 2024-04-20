using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Services.Interfaces.AuthService;
using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace ReactVeloShop.Server.Controllers.Api
{
    [ApiController]
    [Route("/api/user")]
    public class UserApiController : ControllerBase
    {
        private IUserService _userService;
        private IHttpContextAccessor _httpContextAccessor;
        private IAuthService _authService;
        private IJwtProvider _jwtProvider;
        public UserApiController(IUserService userService, IHttpContextAccessor httpContextAccessor, IAuthService authService, IJwtProvider jwtProvider)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserData data)
        {
            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginUserData data)
        {
            return Ok();
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            return Ok();
        }

        [HttpGet]
        [Route("refresh")]
        public ActionResult RefreshToken()
        {
            
            return Ok();
        }

        [Authorize(Policy = "User")]
        [HttpGet]
        [Route("profile")]
        public ActionResult Profile()
        {
            var userData = (CurrentUserData)_httpContextAccessor.HttpContext.Items["UserData"];

            if(userData == null)
            {
                return NotFound("User not fount");
            }

            return Ok(userData);
        }
    }
}
