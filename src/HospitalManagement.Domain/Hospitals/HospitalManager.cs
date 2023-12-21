using System;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagement.Departments;
using HospitalManagement.Doctors;

using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HospitalManagement.Hospitals
{
    public class HospitalManager : DomainService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public HospitalManager(
            IHospitalRepository hospitalRepository, 
            IRepository<Department, Guid> departmentRepository)
        {
            _hospitalRepository = hospitalRepository;
            _departmentRepository = departmentRepository;
        }

        public async Task CreateAsync(string name, [CanBeNull] string[] departmentNames)
        {
            var hospital = new Hospital(GuidGenerator.Create(), name);
            await SetDepartmentsAsync(hospital, departmentNames);
            await _hospitalRepository.InsertAsync(hospital);
        }

        public async Task UpdateAsync(Hospital hospital,string name, [CanBeNull] string[] departmentNames)
        {   
            hospital.SetName(name);
            await SetDepartmentsAsync(hospital, departmentNames);
            await _hospitalRepository.UpdateAsync(hospital);
        }

        private async Task SetDepartmentsAsync(Hospital hospital, [CanBeNull] string[] departmentNames)
        {
            if (departmentNames == null || !departmentNames.Any())
            {
                hospital.RemoveAllDepartments();
                return;
            }

            var query = (await _departmentRepository.GetQueryableAsync())
                .Where(x => departmentNames.Contains(x.Name))
                .Select(x => x.Id)
                .Distinct();

            var departmentIds = await AsyncExecuter.ToListAsync(query);
            if (!departmentIds.Any())
            {
                return;
            }

            hospital.RemoveAllDepartmentsExceptGivenIds(departmentIds);

            foreach (var departmentId in departmentIds)
            {
                hospital.AddDepartment(departmentId);
            }
        }
    }
}