using FLCManagementProject.Models;
using FLCManagementProject.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.userPassword);

            var newUser = new User
            {
                UserFName = request.userFName,
                UserLName = request.userLName,
                UserEmail = request.userEmail,
                UserPhone = request.userPhone,
                UserDob = request.userDob,
                PasswordHash = passwordHash,
                RoleID = 3
            };

            _userService.AddUser(newUser);

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            var user = _userService.GetUserByEmail(request.userEmail);

            if (user == null)
            {
                return BadRequest("Không tìm thấy người dùng.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.userPassword, user.PasswordHash))
            {
                return BadRequest("Sai mật khẩu.");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        [HttpGet("userinfo")]
        [Authorize]
        public async Task<ActionResult<User>> GetUserInfo()
        {
            var userEmail = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;

            if (userEmail == null)
            {
                return BadRequest("Không thể lấy thông tin người dùng.");
            }

            var userInfo = await _userService.GetUserInfoAsync(userEmail);

            if (userInfo == null)
            {
                return NotFound("Không tìm thấy thông tin người dùng.");
            }

            return Ok(userInfo);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new byte[64]; // 512 bits = 64 bytes
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            var symmetricKey = new SymmetricSecurityKey(key);

            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
