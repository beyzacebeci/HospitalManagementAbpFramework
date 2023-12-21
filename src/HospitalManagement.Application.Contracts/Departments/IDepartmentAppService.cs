using HospitalManagement.Doctors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HospitalManagement.Departments
{
    public interface IDepartmentAppService :
            ICrudAppService<DepartmentDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateDepartmentDto, CreateUpdateDepartmentDto>
    {
        //Task<PagedResultDto<DepartmentDto>> GetListAsync(DepartmentGetListInput input);
        //Task<DepartmentDto> GetAsync(Guid id);
        //Task CreateAsync(CreateUpdateDepartmentDto input);
        //Task UpdateAsync(Guid id, CreateUpdateDepartmentDto input);
        //Task DeleteAsync(Guid id);
        //Task<ListResultDto<DoctorLookupDto>> GetDoctorLookupAsync();
    }
}
