using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;

namespace HospitalManagement.Departments
{
    public class DepartmentWithDetails 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string[] DoctorNames { get; set; }

    }
}
