using HospitalManagement.Doctors;
using HospitalManagement.EntityFrameworkCore;
using HospitalManagement.Hospitals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HospitalManagement.Departments
{
    public class EfCoreDepartmentRepository :
        EfCoreRepository<HospitalManagementDbContext, Department, Guid>, IDepartmentRepository
    {
        public EfCoreDepartmentRepository(IDbContextProvider<HospitalManagementDbContext> dbContextProvider)
             : base(dbContextProvider)
        {
        }
        public async Task<List<DepartmentWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();

            
            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Department.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
        private async Task<IQueryable<DepartmentWithDetails>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync())
                .Include(x => x.Doctors)
                .Select(x => new DepartmentWithDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    DoctorNames = x.Doctors
                            .Join(dbContext.Set<Doctor>(),
                            hospitalDepartmentDoctor => hospitalDepartmentDoctor.DoctorId,
                            doctor => doctor.Id,
                            (hospitalDepartmentDoctor, doctor) => doctor.Name).ToArray()
                });

        }
        public async Task<DepartmentWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();

            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }



        public override Task<IQueryable<Department>> WithDetailsAsync()
        {
            return base.WithDetailsAsync(x => x.Doctors);
        }

    }
}
