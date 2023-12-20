using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace HospitalManagement.Hospitals
{
    public class HospitalDto : EntityDto<Guid>
    {
        public string Name{get;set;}

        [CanBeNull]
        public string[] DepartmentNames { get; set; }
    }
}
