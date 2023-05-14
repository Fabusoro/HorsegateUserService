using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Domain.Dtos;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminOnly")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegistrationDto model)
        {
            var result = await _authService.RegisterAdmin(model);
            return StatusCode(result.StatusCode, result);
        }
        
        [HttpPost("register-teacher")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminOnly")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegistrationDto model)
        {
            var result = await _authService.RegisterTeacher(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("register-student")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequiresTeachersAndAdmin")]
        public async Task<IActionResult> RegisterStudent([FromBody] RegistrationDto model)
        {
            if(!ModelState.IsValid){
                return BadRequest("Invalid input");
            }
            var result = await _authService.RegisterStudent(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.Login(model);
            return StatusCode(result.StatusCode, result);
        }
    }
}