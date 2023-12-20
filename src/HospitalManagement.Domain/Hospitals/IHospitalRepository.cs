using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HospitalManagement.Hospitals

{
	public interface IHospitalRepository : IRepository<Hospital, Guid>
	{
		Task<List<HospitalWithDetails>> GetListAsync(
			string sorting,
			int skipCount,
			int maxResultCount,
			CancellationToken cancellationToken = default
		);

		Task<HospitalWithDetails> GetAsync(Guid id, CancellationToken cancellationToken = default);
	}
}