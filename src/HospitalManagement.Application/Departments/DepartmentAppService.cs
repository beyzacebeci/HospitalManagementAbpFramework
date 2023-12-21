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
    public class DepartmentAppService : CrudAppService<Department, DepartmentDto, 
        Guid, PagedAndSortedResultRequestDto, CreateUpdateDepartmentDto, 
        CreateUpdateDepartmentDto>, IDepartmentAppService
    {
       public DepartmentAppService(IRepository<Department, Guid> repository) 
            : base(repository) 
        { 
        }

    }
}
