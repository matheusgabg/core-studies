using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCore.Controllers.Resources;
using WebApplicationCore.Domain.Security.Tokens;
using WebApplicationCore.Domain.Services;

namespace WebApplicationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [Route("/api/login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(accessTokenResource);
        }

        [Route("/api/token/refresh")]
        [HttpPost]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenResource refreshTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authenticationService.RefreshTokenAsync(refreshTokenResource.Token, refreshTokenResource.UserEmail);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var tokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            return Ok(tokenResource);
        }

        [Route("/api/token/revoke")]
        [HttpPost]
        public IActionResult RevokeToken([FromBody] RevokeTokenResource revokeTokenResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _authenticationService.RevokeRefreshToken(revokeTokenResource.Token);
            return NoContent();
        }
    }
}