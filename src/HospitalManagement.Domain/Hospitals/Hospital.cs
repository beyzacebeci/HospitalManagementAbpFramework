using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace HospitalManagement.Hospitals
{
	public class Hospital : FullAuditedAggregateRoot<Guid>
	{
		public string Name { get; private set; }
		public ICollection<HospitalDepartment> HospitalDepartments { get; private set; }
        private Hospital()
		{
		}

		public Hospital(Guid id, string name)
			: base(id)
		{
			SetName(name);
            HospitalDepartments = new Collection<HospitalDepartment>();

        }

		public void SetName(string name)
		{
			Name = Check.NotNullOrWhiteSpace(name, nameof(name), HospitalConsts.MaxNameLength);
		}

		public void AddDepartment(Guid departmentId)
		{
			Check.NotNull(departmentId, nameof(departmentId));

			if (IsInDepartment(departmentId))
			{
				return;
			}

			HospitalDepartments.Add(new HospitalDepartment(hospitalId: Id, departmentId));
		}

        public void RemoveDepartment(Guid departmentId)
		{
			Check.NotNull(departmentId, nameof(departmentId));

			if (!IsInDepartment(departmentId))
			{
				return;
			}

			HospitalDepartments.RemoveAll(x => x.DepartmentId == departmentId);
		}

		public void RemoveAllDepartmentsExceptGivenIds(List<Guid> departmentIds)
		{
			Check.NotNullOrEmpty(departmentIds, nameof(departmentIds));

			HospitalDepartments.RemoveAll(x => !departmentIds.Contains(x.DepartmentId));
		}

		public void RemoveAllDepartments()
		{
			HospitalDepartments.RemoveAll(x => x.HospitalId == Id);
		}

		private bool IsInDepartment(Guid departmentId)
		{
			return HospitalDepartments.Any(x => x.DepartmentId == departmentId);
		}

    }
}