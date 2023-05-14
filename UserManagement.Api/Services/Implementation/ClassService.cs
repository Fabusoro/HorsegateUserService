using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using UserManagement.Api.Domain;
using UserManagement.Api.Domain.Dtos;
using UserManagement.Api.Repository.Interface;
using UserManagement.Api.Services.Interface;

namespace UserManagement.Api.Services
{
    public class ClassService : IClassService
    {
         private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ResponseDto<IEnumerable<ClassDto>>> GetAllClassesAsync()
        {
            var classes = await _unitOfWork.ClassRepository.GetAllAsync();
            if (classes == null)
            {
                return ResponseDto<IEnumerable<ClassDto>>.Fail("Classes Not Found", (int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<IEnumerable<ClassDto>>(classes);
            return ResponseDto<IEnumerable<ClassDto>>.Success("Classes Found", result, (int)HttpStatusCode.OK);
        }

        public async Task<ResponseDto<ClassDto>> GetClassByIdAsync(string id)
        {
            var entity = await _unitOfWork.ClassRepository.GetAsync(e => e.Id == id);
            if (entity == null)
            {
                return ResponseDto<ClassDto>.Fail("Class not found", (int)HttpStatusCode.NotFound);
            }
            var result = _mapper.Map<ClassDto>(entity);
            return ResponseDto<ClassDto>.Success("Class found", result, (int)HttpStatusCode.OK);
        }

        public async Task AddClassAsync(ClassDto classDto)
        {
            var entity = _mapper.Map<Class>(classDto);
            await _unitOfWork.ClassRepository.InsertAsync(entity);
            _unitOfWork.SaveAsync();
        }

        public void UpdateClass(ClassDto classDto)
        {
            var entity = _mapper.Map<Class>(classDto);
            classDto.Name = entity.Name;
            entity.UpdatedAt = DateTimeOffset.Now;
            _unitOfWork.ClassRepository.Update(entity);
            _unitOfWork.SaveAsync();
        }

        public async Task DeleteClass(string id)
        {
            var entity = _unitOfWork.ClassRepository.GetAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new Exception("class not found");
            }
            await _unitOfWork.ClassRepository.DeleteAsync(id);
            _unitOfWork.SaveAsync();
        }
    }
}