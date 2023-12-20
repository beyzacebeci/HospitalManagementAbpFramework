using HospitalManagement.Hospitals;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace HospitalManagement.Doctors
{
	public class Doctor : FullAuditedAggregateRoot<Guid>
	{
		public string Name { get; private set; }

		public DateTime BirthDate { get; set; }
		public string ShortBio { get; set; }

        private Doctor()
		{
            /* This constructor is for deserialization / ORM purpose */
		}
        public Doctor(Guid id, string name, [CanBeNull] DateTime birthDate, [CanBeNull] string shortBio= null) : base(id) 
		{ 
			SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }
        public Doctor SetName(string name) 
		{ 
			Name = Check.NotNullOrWhiteSpace(name, nameof(name), 
			DoctorConsts.MaxNameLength); 
			return this; 
		}


    }
}
