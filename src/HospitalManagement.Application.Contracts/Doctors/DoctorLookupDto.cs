using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HospitalManagement.Doctors
{
    public class DoctorLookupDto : EntityDto<Guid>
    {
        public string Name {get;set;}
}
}
