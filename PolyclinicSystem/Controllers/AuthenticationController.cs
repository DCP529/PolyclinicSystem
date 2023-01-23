using AuthenticationOptions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Models.ModelsDb;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private AuthenticationService _authenticationService;
        private readonly IOptions<AuthOptions> _authOptions;
        private IMapper _mapper;


        public AuthenticationController(IMapper mapper,IOptions<AuthOptions> options, PolyclinicDbContext dbContext)
        {
            _mapper = mapper;
            _authenticationService = new AuthenticationService(_mapper, dbContext);
            _authOptions = options;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            var account = await _authenticationService.AuthenticateAccount(login);

            if (account != null)
            {
                var token = _authenticationService.GenerateJWT(account, _authOptions);

                return Ok(new
                {
                    access_token = token
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(Login login, string repeatPassword)
        {
            return await _authenticationService.RegisterAccount(login, repeatPassword);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("getRole")]
        public async Task<ActionResult<bool>> GetRole()
        {            
            return true;
        }
    }
}
