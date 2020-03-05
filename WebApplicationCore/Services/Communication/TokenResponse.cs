using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Domain.Security.Tokens;

namespace WebApplicationCore.Services.Communication
{
    public class TokenResponse : BaseResponse
    {
        public AccessToken Token { get; set; }

        public TokenResponse(bool success, string message, AccessToken token) : base(success, message)
        {
            Token = token;
        }
    }
}
