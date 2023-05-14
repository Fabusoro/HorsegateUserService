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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;            
        }

        [HttpGet("Get-Users")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminOnly")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("Get-Teachers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequiresTeachersAndAdmin")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var result = await _userService.GetAllTeachersAsync();
            return Ok(result);
        }

        [HttpGet("Get-Students")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequiresTeachersAndAdmin")]
        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _userService.GetAllStudentsAsync();
            return Ok(result);
        }

        [HttpGet("Get-StudentsByClassId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequiresTeachersAndAdmin")]
        public async Task<IActionResult> GetStudentsByClassId([FromQuery]string id)
        {
            var result = await _userService.GetStudentsByClassId(id);
            return Ok(result);
        }

        [HttpGet("Get-UserById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequiresTeacherAdminStudent")]
        public async Task<IActionResult> GetUserById([FromQuery]string id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpDelete("Delete-User")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminOnly")]
        public async Task<IActionResult> DeleteUserByMail([FromQuery] string id)
        {
            var result = await _userService.DeleteUser(id);
            return Ok(result);
        }

        [HttpPatch("Update-User")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminOnly")]
        public async Task<IActionResult> UpdateUser([FromQuery]string id, UpdateDto updateDto)
        {
            var result = await _userService.UpdateUserById(id, updateDto);
            return Ok(result);
        }
    }
}