using System;
using System.Collections.Generic;
using System.Text;

namespace HospitalManagement.Doctors
{
    public class CreateUpdateDoctorDto
    {
        public string Name {get;set;}
        public DateTime BirthDate {get;set;}
        public string ShortBio {get; set;}
    }
}
