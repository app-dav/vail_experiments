﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace JWT.Experiment._1.Test
{
    public class PlayingWithTokens
    {
        [Fact]
        public void TestSelfValidate()
        {
            string secretKey = "mysupersecret_secretkey!123";
            string url = "http://vailresorts.com";

            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.Now;

            var descr = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Email, "dappel@vailresorts.com")
                    }),
                Issuer = "vailResorts",
                Audience = url,
                IssuedAt = now,
                Expires = now.AddMinutes(60),
                NotBefore = now.AddMinutes(45),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(descr);

            var stringyToken = tokenHandler.WriteToken(token);

            Assert.NotNull(stringyToken);

            var validators = new TokenValidationParameters();
            SecurityToken validToken = null;
            tokenHandler.ValidateToken(stringyToken, validators, out validToken);
            Assert.NotNull(validToken);
            
        }
    }
}
