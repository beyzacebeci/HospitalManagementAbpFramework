using System;
using Volo.Abp.Auditing;

namespace HospitalManagement.Hospitals
{
	public class HospitalWithDetails : IHasCreationTime
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public string[] DepartmentNames { get; set; }

		public DateTime CreationTime { get; set; }
	}
}