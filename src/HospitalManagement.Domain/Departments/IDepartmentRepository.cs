using HospitalManagement.Doctors;
using HospitalManagement.Hospitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HospitalManagement.Departments
{
    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<List<DepartmentWithDetails>> GetListAsync(
           string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
            );
        Task<DepartmentWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
