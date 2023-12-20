using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using HospitalManagement.Hospitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;


namespace HospitalManagement.Departments
{
    public class DepartmentAppService : 
        HospitalManagementAppService,
        IDepartmentAppService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly DepartmentManager _departmentManager;
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IRepository<Doctor, Guid> _doctorRepository;
        
        public DepartmentAppService(
            IDepartmentRepository departmentRepository,
            DepartmentManager departmentManager,
            IHospitalRepository hospitalRepository,
            IRepository<Doctor, Guid> doctorRepository
            )

        {
            _departmentRepository = departmentRepository;
            _departmentManager = departmentManager;
            _hospitalRepository = hospitalRepository;
            _doctorRepository = doctorRepository;

        }

        public async Task<PagedResultDto<DepartmentDto>> GetListAsync(DepartmentGetListInput input)
        {
            var departments = await _departmentRepository.GetListAsync(input.Sorting, input.SkipCount, input.MaxResultCount);
            var totalCount = await _departmentRepository.CountAsync();

            return new PagedResultDto<DepartmentDto>(
                totalCount,
                ObjectMapper.Map<List<DepartmentWithDetails>, List<DepartmentDto>>(departments));
        }

        public async Task<DepartmentDto> GetAsync(Guid id)
        {
            var department = await _departmentRepository.GetAsync(id);

            return ObjectMapper.Map<DepartmentWithDetails, DepartmentDto>(department);
        }


        public async Task CreateAsync(CreateUpdateDepartmentDto input)
        {
            await _departmentManager.CreateAsync(
                input.Name,
                input.DoctorNames
            );
        }

        public async Task UpdateAsync(Guid id, CreateUpdateDepartmentDto input)
        {
            var department = await _departmentRepository.GetAsync(id, includeDetails: true);

            await _departmentManager.UpdateAsync(
                department,
                input.Name,
                input.DoctorNames
            );
        }

        public async Task DeleteAsync(Guid id)
        {
            await _departmentRepository.DeleteAsync(id);
        }

        public async Task<ListResultDto<DoctorLookupDto>> GetDoctorLookupAsync()
        {
            var doctors = await _doctorRepository.GetListAsync();

            return new ListResultDto<DoctorLookupDto>(
                ObjectMapper.Map<List<Doctor>, List<DoctorLookupDto>>(doctors)
            );
        }

    }
}
