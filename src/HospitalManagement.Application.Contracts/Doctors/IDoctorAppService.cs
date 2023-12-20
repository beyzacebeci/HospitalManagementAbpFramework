using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HospitalManagement.Doctors
{
    public interface IDoctorAppService :
        ICrudAppService<
            DoctorDto, 
            Guid, 
            PagedAndSortedResultRequestDto, 
            CreateUpdateDoctorDto, 
            CreateUpdateDoctorDto>
    {
    }
}
