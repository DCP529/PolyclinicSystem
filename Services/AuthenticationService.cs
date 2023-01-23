
using AuthenticationOptions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.ModelsDb;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services
{
    public class AuthenticationService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;

        public AuthenticationService(IMapper mapper, PolyclinicDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Account> AuthenticateAccount(Login login)
        {
            return _mapper.Map<Account>(await _dbContext.Accounts
                .Where(x => x.Email == login.Email && x.Password == login.Password).Include(x => x.Role)
                .FirstOrDefaultAsync());
        }

        public async Task<IActionResult> RegisterAccount(Login login, string repeatPassword)
        {
            if (login.Password != repeatPassword)
            {
                return new BadRequestResult();
            }

            var role = new RoleDb()
            {
                RoleName = "User",
                Id = Guid.NewGuid()
            };

            var account = new AccountDb()
            {
                Id = Guid.NewGuid(),
                Email = login.Email,
                Password = login.Password,
                RoleId = role.Id,
                Role = role
            };

            var accountDb = await _dbContext.Accounts.Where(x => x.Email == login.Email && x.Password == login.Password)
                .FirstOrDefaultAsync();

            if (accountDb == null)
            {
                await _dbContext.Roles.AddAsync(role);
                await _dbContext.Accounts.AddAsync(account);
                await _dbContext.SaveChangesAsync();

                return new StatusCodeResult(200);
            }

            return new BadRequestResult();
        }

        public string GenerateJWT(Account account, IOptions<AuthOptions> authOptions)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var creditials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString())
            };

            var role = _dbContext.Roles.Where(x => x.Id == account.RoleId).FirstOrDefault();

            claims.Add(new Claim("role", role.RoleName));

            var token = new JwtSecurityToken(authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: creditials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
