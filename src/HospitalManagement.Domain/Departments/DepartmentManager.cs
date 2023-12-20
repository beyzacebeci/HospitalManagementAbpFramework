using HospitalManagement.Doctors;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HospitalManagement.Departments
{
    public class DepartmentManager : DomainService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRepository<Doctor, Guid> _doctorRepository; 
        public DepartmentManager(IDepartmentRepository departmentRepository, 
            IRepository<Doctor, Guid> doctorRepository)
        { 
            _departmentRepository = departmentRepository; 
            _doctorRepository = doctorRepository; 
        }

        public async Task CreateAsync(string name, [CanBeNull] string[] doctorNames) 
        { 
            var department = new Department(GuidGenerator.Create(), name); 
            
            await SetDoctorsAsync(department, doctorNames); 
           
            await _departmentRepository.InsertAsync(department); 
        }

        public async Task UpdateAsync(
            Department department, 
            string name,  
            [CanBeNull] string[] doctorNames)
        { 
            department.SetName(name); 
          
            await SetDoctorsAsync(department, doctorNames); 
            await _departmentRepository.UpdateAsync(department);
        }


        private async Task SetDoctorsAsync(Department department, [CanBeNull] string[] doctorNames) 
        { 
            if (doctorNames == null || !doctorNames.Any()) 
            { 
                department.RemoveAllDoctors(); return; 
            } 
            var query = (await _doctorRepository
                .GetQueryableAsync())
                .Where(x => doctorNames.Contains(x.Name))
                .Select(x => x.Id)
                .Distinct(); 
           
            var doctorIds = await AsyncExecuter.ToListAsync(query); 
            
            if (!doctorIds.Any()) 
            { 
                return; 
            } 
            
            department.RemoveAllDoctorsExceptGivenIds(doctorIds); 
            
            foreach (var doctorId in doctorIds)
            { 
                department.AddDoctor(doctorId);
            } 
        }
    }
}
