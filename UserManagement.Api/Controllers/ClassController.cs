using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Api.Domain.Dtos;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet("GetAllClass")]        
        public async Task<IActionResult> GetAllClass()
        {
            var result = await _classService.GetAllClassesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(string id)
        {
            var result = _classService.GetClassByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        
        public async Task<IActionResult> AddClass(ClassDto classDto)
        {
            if(!ModelState.IsValid){
                return BadRequest("invalid input");
            }
            await _classService.AddClassAsync(classDto);
            return Ok("class added");
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateClass(ClassDto classDto)
        {
            if(!ModelState.IsValid){
                return BadRequest("invalid input");
            }
            _classService.UpdateClass(classDto);
            return Ok("Class Updated");
        }

        [HttpDelete("{id}"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            await _classService.DeleteClass(id);
            return Ok("Class Deleted");
        }
    }
}