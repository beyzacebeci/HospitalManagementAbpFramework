using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using HospitalManagement.Departments;
using HospitalManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HospitalManagement.Hospitals
{
    public class EfCoreHospitalRepository :
        EfCoreRepository<HospitalManagementDbContext, Hospital, Guid>, IHospitalRepository
    {
        public EfCoreHospitalRepository(IDbContextProvider<HospitalManagementDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<List<HospitalWithDetails>> GetListAsync(
            string sorting,
            int skipCount,
            int maxResultCount,
            CancellationToken cancellationToken = default
        )
        {
            var query = await ApplyFilterAsync();

            return await query
                .OrderBy(!string.IsNullOrWhiteSpace(sorting) ? sorting : nameof(Hospital.Name))
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public async Task<HospitalWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var query = await ApplyFilterAsync();

            return await query
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync(GetCancellationToken(cancellationToken));
        }

        private async Task<IQueryable<HospitalWithDetails>> ApplyFilterAsync()
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync())
                .Include(x => x.HospitalDepartments)
                .Select(x => new HospitalWithDetails
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreationTime = x.CreationTime,
                    DepartmentNames = (from hospitalDepartments in x.HospitalDepartments
                                     join department in dbContext.Set<Department>() on hospitalDepartments.DepartmentId equals department.Id
                                     select department.Name).ToArray()
                });
        }

        public override Task<IQueryable<Hospital>> WithDetailsAsync()
        {
            return base.WithDetailsAsync(x => x.HospitalDepartments);
        }
    }
}