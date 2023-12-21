using HospitalManagement.Departments;
using HospitalManagement.Hospitals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace HospitalManagement.Departments
{
	public class Department : AuditedAggregateRoot<Guid>
	{
		public string Name { get; private set; }
        //public ICollection<HospitalDepartmentDoctor> Doctors { get; private set; }

       
        /* This constructor is for deserialization / ORM purpose */
        private Department()
		{
		}

		public Department(Guid id, string name) : base(id)
		{
			SetName(name);
			//Doctors = new Collection<HospitalDepartmentDoctor>();
        }

        public Department SetName(string name)
		{
			Name = Check.NotNullOrWhiteSpace(name, nameof(name), DepartmentConsts.MaxNameLength);
			return this;
		}


        //public void AddDoctor(Guid doctorId)
        //{
        //    Check.NotNull(doctorId, nameof(doctorId));

        //    if (IsInDoctor(doctorId))
        //    {
        //        return;
        //    }

            
        //    Doctors.Add(new HospitalDepartmentDoctor(hospitalId: Id, departmentId : Id,doctorId));
        //}

        //public void RemoveDoctor(Guid doctorId)
        //{
        //    Check.NotNull(doctorId, nameof(doctorId));

        //    if (!IsInDoctor(doctorId))
        //    {
        //        return;
        //    }

        //    Doctors.RemoveAll(x => x.DoctorId == doctorId);
        //}

        //public void RemoveAllDoctorsExceptGivenIds(List<Guid> doctorIds)
        //{
        //    Check.NotNullOrEmpty(doctorIds, nameof(doctorIds));

        //    Doctors.RemoveAll(x => !doctorIds.Contains(x.DoctorId));
        //}

        //public void RemoveAllDoctors()
        //{
        //    Doctors.RemoveAll(x => x.DepartmentId == Id);
        //}

        //private bool IsInDoctor(Guid doctorId)
        //{
        //    return Doctors.Any(x => x.DoctorId == doctorId);
        //}
    }
}