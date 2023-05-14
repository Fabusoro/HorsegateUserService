using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserManagement.Api.Domain;
using UserManagement.Api.Domain.Dtos;
using UserManagement.Api.Repository.Interface;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Users>? _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;       

        public AuthService(UserManager<Users> userManager, RoleManager<IdentityRole> roleManager, ITokenService tokenService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<RegistrationResponseDto>> RegisterAdmin(RegistrationDto userDetails)
        {
            var checkEmail = await _userManager.FindByEmailAsync(userDetails.Email);
            if (checkEmail != null)
            {
                return ResponseDto<RegistrationResponseDto>.Fail("Email already Exists", (int)HttpStatusCode.BadRequest);
            }
            var userModel = _mapper.Map<Users>(userDetails);
            userModel.UserName = userDetails.Email;
            userModel.roles = Roles.Admin;
            await _userManager.CreateAsync(userModel, userDetails.Password);            
            
            if (!await _roleManager.RoleExistsAsync(UserRole.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
                           
            if (await _roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(userModel, UserRole.Admin.ToString());
            }
            _unitOfWork.SaveAsync();

            return ResponseDto<RegistrationResponseDto>.Success("Registration Successful",
                new RegistrationResponseDto{FirstName = userModel.FirstName,Email = userModel.Email },
                (int)HttpStatusCode.Created);
        }

        public async Task<ResponseDto<RegistrationResponseDto>> RegisterTeacher(RegistrationDto userDetails)
        {
            var checkEmail = await _userManager.FindByEmailAsync(userDetails.Email);
            if(checkEmail != null){
                return ResponseDto<RegistrationResponseDto>.Fail("Email already Exists",(int)HttpStatusCode.BadRequest);
            }

            var userModel = _mapper.Map<Users>(userDetails);
            userModel.UserName = userDetails.Email;
            userModel.roles = Roles.Teacher;
            await _userManager.CreateAsync(userModel, userDetails.Password);             

            if(!await _roleManager.RoleExistsAsync(UserRole.Teacher)){
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Teacher));
            }  

            if(await _roleManager.RoleExistsAsync(UserRole.Teacher)){
                await _userManager.AddToRoleAsync(userModel, UserRole.Teacher);
            }
            _unitOfWork.SaveAsync();

            return ResponseDto<RegistrationResponseDto>.Success("Registration Successful",
                new RegistrationResponseDto{FirstName = userModel.FirstName,Email = userModel.Email },
                (int)HttpStatusCode.Created);                      
        }

        public async Task<ResponseDto<RegistrationResponseDto>> RegisterStudent(RegistrationDto userDetails)
        {
            var checkEmail = await _userManager.FindByEmailAsync(userDetails.Email);
            if(checkEmail != null)
            {
                return ResponseDto<RegistrationResponseDto>.Fail("Email already exists", (int)HttpStatusCode.BadRequest);
            }

            var userModel = _mapper.Map<Users>(userDetails);
            userModel.UserName = userDetails.Email;
            userModel.roles = Roles.Student;
            await _userManager.CreateAsync(userModel, userDetails.Password);

            if(!await _roleManager.RoleExistsAsync(UserRole.Student)){
                await _roleManager.CreateAsync(new IdentityRole (UserRole.Student));
            } 
            await _userManager.AddToRoleAsync(userModel, UserRole.Student);      

             _unitOfWork.SaveAsync();      

            return ResponseDto<RegistrationResponseDto>.Success("Registration Successful", 
            new RegistrationResponseDto{FirstName = userModel.FirstName, Email = userModel.Email}, (int)HttpStatusCode.Created);            
        }

        public async Task<ResponseDto<CredentialResponseDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ResponseDto<CredentialResponseDto>.Fail("User does not exist", (int)HttpStatusCode.NotFound);
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return ResponseDto<CredentialResponseDto>.Fail("Invalid user credential", (int)HttpStatusCode.BadRequest);
            }            
            
            user.RefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); //sets refresh token for 7 days
            var credentialResponse = new CredentialResponseDto()
            {
                Id = user.Id,
                roles = user.roles,
                classId = user.ClassId,
                Token = await _tokenService.GenerateToken(user),
                RefreshToken = user.RefreshToken
            };


            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {            
                return ResponseDto<CredentialResponseDto>.Success("Login successful", credentialResponse);
            }
            return ResponseDto<CredentialResponseDto>.Fail("Failed to login user", (int)HttpStatusCode.InternalServerError);
        }        
        
    }
}