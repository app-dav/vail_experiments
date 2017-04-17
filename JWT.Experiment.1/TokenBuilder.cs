using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT.Experiment._1
{
    //https://stormpath.com/blog/token-authentication-asp-net-core
    //https://blogs.msdn.microsoft.com/webdev/2016/10/27/bearer-token-authentication-in-asp-net-core/
    //https://pfelix.wordpress.com/2012/11/27/json-web-tokens-and-the-new-jwtsecuritytokenhandler-class/
    //https://docs.microsoft.com/en-us/aspnet/aspnet/overview/owin-and-katana/owin-oauth-20-authorization-server
    public class TokenBuilder
    {
        public string BuildToken()
        {
            string secretKey = "mysupersecret_secretkey!123";
            string url = "http://vailresorts.com";

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.Now;
            var issuer = "vailResorts";

            var descr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, "dappel@vailresorts.com")
                    }),
                Issuer = issuer,
                Audience = url,
                IssuedAt = now,
                Expires = now.AddMinutes(60),
                NotBefore = now,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descr);

            return tokenHandler.WriteToken(token);
        }

    }
}
