using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using UserManagement.Api.Domain.Dtos;

namespace UserManagement.Api.Services.Interface
{
    public interface IUserService
    {
        Task<string> DeleteUser(string id);
        Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllUsersAsync();
        Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllStudentsAsync();
        Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllTeachersAsync();
        Task<ResponseDto<UserResponseDto>> GetUserById(string id);
        Task<ResponseDto<IdentityResult>> UpdateUserById(string id, UpdateDto updateDto);
        Task<ResponseDto<IEnumerable<UserResponseDto>>> GetStudentsByClassId(string id);
    }
}