using HospitalManagement.Departments;
using System;
using Volo.Abp.Domain.Entities;

namespace HospitalManagement.Hospitals
{
	public class HospitalDepartment : Entity
	{
		public Guid HospitalId { get; protected set; }

		public Guid DepartmentId { get; protected set; }

 
        /* This constructor is for deserialization / ORM purpose */
        private HospitalDepartment()
		{
		}

		public HospitalDepartment(Guid hospitalId, Guid departmentId)
		{
			HospitalId = hospitalId;
			DepartmentId = departmentId;
		}

		public override object[] GetKeys()
		{
			return new object[] { HospitalId, DepartmentId };
		}
	}
}