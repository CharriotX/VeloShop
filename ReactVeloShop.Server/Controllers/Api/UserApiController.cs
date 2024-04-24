using Data.Interface.DataModels.Tokens;
using Data.Interface.DataModels.Users;
using Data.Interface.Models;
using Data.Interface.Models.enums;
using Data.Services.Interfaces.AuthService;
using Data.Services.Interfaces.UsersService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

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

            var isEmailExist = _userService.IsEmailExist(data.Email);

            if (isEmailExist)
            {
                return BadRequest();
            }

            _authService.Register(data);

            return Ok();
        }


        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginUserData data)
        {
            var user = _userService.IsEmailExist(data.Email);

            if (user == null)
            {
                return Unauthorized("Wrong email");
            }

            var userWithTokens = _authService.Login(data.Email, data.Password);

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
        public ActionResult RefreshToken(AccessTokenData accessTokenData)
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refresh"];
            var principal = _jwtProvider.GetPrincipalFromExpiredToken(accessTokenData.AccessToken);
            var username = principal.Identity?.Name;

            var newTokens = _authService.UpdateRefreshToken(username, refreshToken);

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

        [HttpGet]
        [Authorize(Roles ="Admin")]
        [Route("users")]
        public ActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }
    }
}
