using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HospitalManagement.Doctors
{
    public class DoctorDto : EntityDto<Guid>
    {
        public string Name{get;set;}
        public DateTime BirthDate{get;set;}
        public string ShortBio{get; set;}
    }
}
