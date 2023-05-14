using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain.Dtos;

namespace UserManagement.Api.Services.Interface
{
    public interface IClassService
    {
        Task AddClassAsync(ClassDto classDto);
        Task DeleteClass(string id);
        Task<ResponseDto<IEnumerable<ClassDto>>> GetAllClassesAsync();
        Task<ResponseDto<ClassDto>> GetClassByIdAsync(string id);
        void UpdateClass(ClassDto classDto);
    }
}