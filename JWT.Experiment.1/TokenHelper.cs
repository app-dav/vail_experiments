using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Experiment._1
{
    internal class TokenHelper
    {
        // secretKey contains a secret passphrase only your server knows
        internal const string secretKey = "mysupersecret_secretkey!123";
        internal readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
    }
}
