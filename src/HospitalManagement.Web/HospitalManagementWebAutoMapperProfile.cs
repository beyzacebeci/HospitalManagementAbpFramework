using AutoMapper;
using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using HospitalManagement.Hospitals;
using HospitalManagement.Web.Models;
using Volo.Abp.AutoMapper;

namespace HospitalManagement.Web;

public class HospitalManagementWebAutoMapperProfile : Profile
{
    public HospitalManagementWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<DepartmentLookupDto, DepartmentViewModel>()
            .Ignore(x => x.IsSelected); 
       
        CreateMap<HospitalDto, CreateUpdateHospitalDto>(); 
        CreateMap<DoctorDto, CreateUpdateDoctorDto>(); 
        CreateMap<DepartmentDto, CreateUpdateDepartmentDto>();
    }
}
