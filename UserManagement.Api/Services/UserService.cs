using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagement.Api.Domain;
using UserManagement.Api.Domain.Dtos;
using UserManagement.Api.Repository.Interface;
using UserManagement.Api.Services.ExternalService;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Users>? _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            if (users == null)
            {
                return ResponseDto<IEnumerable<RegistrationResponseDto>>.Fail("Teachers Not Found", (int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<IEnumerable<RegistrationResponseDto>>(users);
            return ResponseDto<IEnumerable<RegistrationResponseDto>>.Success("Teachers Found", result, (int)HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllTeachersAsync()
        {
            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            if (teachers == null)
            {
                return ResponseDto<IEnumerable<RegistrationResponseDto>>.Fail("Teachers Not Found", (int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<IEnumerable<RegistrationResponseDto>>(teachers);
            return ResponseDto<IEnumerable<RegistrationResponseDto>>.Success("Teachers Found", result, (int)HttpStatusCode.OK);
        }

        public async Task<ResponseDto<IEnumerable<RegistrationResponseDto>>> GetAllStudentsAsync()
        {
            var students = await _userManager.GetUsersInRoleAsync("Student");
            if (students == null)
            {
                return ResponseDto<IEnumerable<RegistrationResponseDto>>.Fail(" Students Not Found", (int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<IEnumerable<RegistrationResponseDto>>(students);
            return ResponseDto<IEnumerable<RegistrationResponseDto>>.Success("Students Found", result, (int)HttpStatusCode.OK);
        }

        public async Task<string> DeleteUser(string id)
        {
            var entity = await _userManager.FindByIdAsync(id);
            if (entity == null)
            {
                return "user not found";
            }
            var result = await _userManager.DeleteAsync(entity);
            return "user deleted";
        }

        public async Task<ResponseDto<IEnumerable<UserResponseDto>>> GetStudentsByClassId(string id)
        {
            var user = await _unitOfWork.UserRepository.GetAll(e => e.ClassId == id && e.roles == Roles.Student);
            if (user == null)
            {
                return ResponseDto<IEnumerable<UserResponseDto>>.Fail("Students Not found", (int)HttpStatusCode.NotFound);
            }

            var entity = _mapper.Map<IEnumerable<UserResponseDto>>(user);
            return ResponseDto<IEnumerable<UserResponseDto>>.Success("Students Found", entity, (int)HttpStatusCode.OK);
        }

        public async Task<ResponseDto<UserResponseDto>> GetUserById(string id)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(e => e.Id == id);
            if (user == null)
            {
                return ResponseDto<UserResponseDto>.Fail("User Not found", (int)HttpStatusCode.NotFound);
            }

            var httpClient = new MyHttpClient(){
            };
            var paymentStatus = await httpClient.CallOtherServiceAsync(id);
            

            if (paymentStatus == "success")
            {
                user.IsPaid = true;
            }
            else
            {
                user.IsPaid = false;
            }
            var entity = _mapper.Map<UserResponseDto>(user);
            return ResponseDto<UserResponseDto>.Success("Students Found", entity, (int)HttpStatusCode.OK);

        }

        public async Task<ResponseDto<IdentityResult>> UpdateUserById(string id, UpdateDto updateDto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return ResponseDto<IdentityResult>.Fail("User not found", (int)HttpStatusCode.NotFound);
            }
            user.FirstName = updateDto.FirstName;
            user.LastName = updateDto.LastName;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.Email = updateDto.Email;
            user.UpdatedAt = DateTimeOffset.UtcNow;

            var result = await _userManager.UpdateAsync(user);
            return ResponseDto<IdentityResult>.Success("UserUpdated", result, (int)HttpStatusCode.OK);
        }
    }
}