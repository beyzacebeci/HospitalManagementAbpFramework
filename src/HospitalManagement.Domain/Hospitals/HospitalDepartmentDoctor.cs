using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace HospitalManagement.Hospitals
{
    public class HospitalDepartmentDoctor : Entity
    {
        public Guid HospitalId { get; protected set; }
        public Guid DepartmentId { get; protected set; }
        public Guid DoctorId { get; protected set; }
      
        //public Hospital Hospital { get; protected set; }
        //public Department Department { get; protected set; }
        //public Doctor Doctor { get; protected set; }

        private HospitalDepartmentDoctor()
        {

        }
        public HospitalDepartmentDoctor(Guid hospitalId, Guid departmentId,Guid doctorId)
        {
            HospitalId = hospitalId;
            DepartmentId = departmentId;
            DoctorId = doctorId;
        }
        public override object[] GetKeys()
        {
            return new object[] { HospitalId, DepartmentId , DoctorId};
        }
    }
}
