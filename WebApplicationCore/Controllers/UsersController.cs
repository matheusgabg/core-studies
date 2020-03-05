using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCore.Controllers.Resources;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Domain.Services;

namespace WebApplicationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserCredentialsResource userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

            var response = await _userService.CreateUserAsync(user, ERole.Common);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<User, UserResource>(response.User);
            return Ok(userResource);
        }
    }
}