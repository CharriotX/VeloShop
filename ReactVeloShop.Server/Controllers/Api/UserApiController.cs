using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Services.Interfaces.AuthService;
using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            if (data == null)
            {
                return BadRequest();
            }

            var isEmailExist = await _userService.IsEmailExist(data.Email);

            if (isEmailExist)
            {
                return BadRequest();
            }

            _authService.Register(data);

            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginUserData data)
        {
            var user = await _userService.IsEmailExist(data.Email);

            if (user == false)
            {
                return Unauthorized("User with this email was not found");
            }

            var userWithTokens = await _authService.Login(data.Email, data.Password);
            if (userWithTokens == null)
            {
                return Unauthorized();
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh", userWithTokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Ok(userWithTokens);
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Request.Headers.Remove("autorization");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refresh");
            return Ok();
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult> RefreshToken(AccessTokenData accessTokenData)
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refresh"];

            if(refreshToken == null)
            {
                return Unauthorized();
            }

            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessTokenData.AccessToken);
            var username = principal.Identity?.Name;

            var newTokens = await _authService.UpdateRefreshToken(username, refreshToken);

            if (newTokens == null)
            {
                return Unauthorized();
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Delete("refresh");
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh", newTokens.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Ok(newTokens);
        }


        [HttpGet]
        [Route("profile")]
        [Authorize]
        public ActionResult Profile()
        {
            var userData = (CurrentUserData)_httpContextAccessor.HttpContext.Items["UserData"];

            if (userData == null)
            {
                return NotFound("User not fount");
            }

            return Ok(userData);
        }
    }
}
