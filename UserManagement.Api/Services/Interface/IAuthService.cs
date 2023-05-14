using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Api.Domain.Dtos;

namespace UserManagement.Api.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseDto<RegistrationResponseDto>> RegisterAdmin(RegistrationDto userDetails);
        Task<ResponseDto<RegistrationResponseDto>> RegisterTeacher(RegistrationDto userDetails);
        Task<ResponseDto<RegistrationResponseDto>> RegisterStudent(RegistrationDto userDetails);
        Task<ResponseDto<CredentialResponseDto>> Login(LoginDto model);
    }
}