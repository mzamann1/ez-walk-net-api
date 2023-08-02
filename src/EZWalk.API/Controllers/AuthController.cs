using EZWalk.API.DTOs.Auth;
using EZWalk.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EZWalk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }



        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult is { Succeeded: false })
            {
                return BadRequest("Something went wrong");
            }

            if (registerRequestDto.Roles is { Length: > 0 })
            {
                identityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                return identityResult is { Succeeded: true } ? Ok($"User has been registered with roles  {string.Join(',', registerRequestDto.Roles)}") : BadRequest("User has been registered but unable to assign any roles");

            }


            return Ok("User has been registered, Please login");



        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {

            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user is null)
            {
                return BadRequest("Invalid credentials");
            }

            bool validPassword = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);


            if (!validPassword)
            {
                return BadRequest("Invalid credentials");
            }

            var roles = await _userManager.GetRolesAsync(user);

            if (roles is { Count: <= 0 })
            {
                return BadRequest("No roles");
            }

            var jwtToken = _tokenRepository.CreateJwtToken(user, roles.ToList());

            return Ok(new LoginResponseDto()
            {
                JwtToken = jwtToken
            });
        }
    }
}
