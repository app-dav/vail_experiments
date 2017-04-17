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
    public class TokenValidator
    {
        //Need to pull what we validate against 
        string secretKey = "mysupersecret_secretkey!123";
        string url = "http://vailresorts.com";


        public ClaimsPrincipal ValidateToken(string stringyToken)
        {
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var issuer = "vailResorts";

            var tokenHandler = new JwtSecurityTokenHandler();
            var validators = new TokenValidationParameters()
            {
                AudienceValidator = (aud, tkn, vp) => { return aud.Contains(url); },
                IssuerSigningKey = key,
                IssuerValidator = (iss, tkn, vp) => { return issuer; }
            };
            SecurityToken validToken = null;
            return tokenHandler.ValidateToken(stringyToken, validators, out validToken);

        }
    }
}
