using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SelfEducationProjectAPI.Authentication;
using SelfEducationProjectAPI.Entities;
using SelfEducationProjectAPI.ViewModels;

namespace SelfEducationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private OrdersDbContext db;

        public AuthController(OrdersDbContext context, ILogger<AuthController> logger)
        {
            db = context;
            _logger = logger;
        }

        [HttpPost]
        public UserToken Post([FromBody]User user)
        {
            try
            {
                var identity = GetIdentity(user.Email, user.Password);
                if (identity == null)
                {
                    Console.WriteLine("Bad login");
                    return new UserToken
                    {
                        UserName = null,
                        Token = null,
                        Lifetime = 0
                    };
                }

                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.Issuer,
                        audience: AuthOptions.Audience,
                        notBefore: DateTime.UtcNow,
                        claims: identity.Claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                Console.WriteLine($"Email: {identity.Name}\nToken: {encodedJwt}");
                return new UserToken
                {
                    UserName = identity.Name,
                    Token = encodedJwt,
                    Lifetime = AuthOptions.Lifetime
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new UserToken
                {
                    UserName = null,
                    Token = null,
                    Lifetime = 0
                };
            }
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            User user = db.Users.FirstOrDefault(x => x.Email == username && x.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
