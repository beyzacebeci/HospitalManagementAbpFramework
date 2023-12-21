using AutoMapper;
using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using HospitalManagement.Hospitals;

namespace HospitalManagement;

public class HospitalManagementApplicationAutoMapperProfile : Profile
{
    public HospitalManagementApplicationAutoMapperProfile()
    {
        CreateMap<Department, DepartmentDto>(); 
        CreateMap<Department, DepartmentLookupDto>(); 
        CreateMap<CreateUpdateDepartmentDto, Department>(); 
        
        CreateMap<Doctor, DoctorDto>(); 
        CreateMap<Doctor, DoctorLookupDto>(); 
        CreateMap<CreateUpdateDoctorDto, Doctor>(); 
      
        CreateMap<HospitalWithDetails, HospitalDto>();
        CreateMap<Hospital, HospitalDto>();


    }
}
