using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationCore.Domain.Security.Tokens
{
    public class RefreshToken : JsonWebToken
    {
        public RefreshToken(string token, long expiration) : base(token, expiration)
        {
        }
    }
}
