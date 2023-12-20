using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HospitalManagement.Hospitals
{
    public interface IHospitalAppService : IApplicationService
    {
        Task<PagedResultDto<HospitalDto>> GetListAsync(HospitalGetListInput input); 
        Task<HospitalDto> GetAsync(Guid id); 
        Task CreateAsync(CreateUpdateHospitalDto input); 
        Task UpdateAsync(Guid id, CreateUpdateHospitalDto input); 
        Task DeleteAsync(Guid id);
        Task<ListResultDto<DepartmentLookupDto>> GetDepartmentLookupAsync();
        Task<ListResultDto<DoctorLookupDto>> GetDoctorLookupAsync(); 

    }
}
