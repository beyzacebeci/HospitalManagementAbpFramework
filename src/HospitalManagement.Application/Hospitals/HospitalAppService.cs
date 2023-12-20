using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using HospitalManagement.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HospitalManagement.Hospitals
{
    [Authorize(HospitalManagementPermissions.Hospitals.Default)]
    public class HospitalAppService : HospitalManagementAppService, IHospitalAppService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly HospitalManager _hospitalManager;
        private readonly IRepository<Doctor, Guid> _doctorRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public HospitalAppService(
            IHospitalRepository hospitalRepository,
            HospitalManager hospitalManager,
            IRepository<Doctor, Guid> doctorRepository,
            IRepository<Department, Guid> departmentRepository
        )
        {
            _hospitalRepository = hospitalRepository;
            _hospitalManager = hospitalManager;
            _doctorRepository = doctorRepository;
            _departmentRepository = departmentRepository;
        }
        public async Task<PagedResultDto<HospitalDto>> GetListAsync(HospitalGetListInput input)
        {
            var hospitals = await _hospitalRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount);
            var totalCount = await _hospitalRepository.CountAsync();

            return new PagedResultDto<HospitalDto>(
                totalCount, 
                ObjectMapper.Map<List<HospitalWithDetails>, List<HospitalDto>>(hospitals));
        }

        public async Task<HospitalDto> GetAsync(Guid id)
        {
            var hospital = await _hospitalRepository.GetAsync(id);

            return ObjectMapper.Map<HospitalWithDetails, HospitalDto>(hospital);
        }

        [Authorize(HospitalManagementPermissions.Hospitals.Create)]

        public async Task CreateAsync(CreateUpdateHospitalDto input)
        {
            await _hospitalManager.CreateAsync(
                input.Name,
                input.DepartmentNames
            );
        }

        [Authorize(HospitalManagementPermissions.Hospitals.Edit)]
        public async Task UpdateAsync(Guid id, CreateUpdateHospitalDto input)
        {
            var hospital = await _hospitalRepository.GetAsync(id, includeDetails: true);

            await _hospitalManager.UpdateAsync(
                hospital,
                input.Name,
                input.DepartmentNames
            );
        }

        [Authorize(HospitalManagementPermissions.Hospitals.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _hospitalRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<DoctorLookupDto>> GetDoctorLookupAsync()
        {
            var doctors = await _doctorRepository.GetListAsync();

            return new ListResultDto<DoctorLookupDto>(
                ObjectMapper.Map<List<Doctor>, List<DoctorLookupDto>>(doctors)
            );
        }

        public async Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync()
        {
            var departments = await _departmentRepository.GetListAsync();

            return new ListResultDto<DepartmentLookupDto>(
                ObjectMapper.Map<List<Department>, List<DepartmentLookupDto>>(departments)
            );
        }
    }
}