using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace HospitalManagement.Doctors
{
    public class DoctorAppService:
        CrudAppService<Doctor, DoctorDto, Guid, PagedAndSortedResultRequestDto, 
            CreateUpdateDoctorDto, CreateUpdateDoctorDto>, IDoctorAppService
    {
        public DoctorAppService(IRepository<Doctor, Guid> repository) 
            : base(repository) 
        { 
        }
    }
}
