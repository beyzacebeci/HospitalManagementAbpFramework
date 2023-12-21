using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Hospitals
{
    public class CreateUpdateHospitalDto
    {
        public string Name{get;set;}

        public string[] DepartmentNames { get; set; }
    }
}
