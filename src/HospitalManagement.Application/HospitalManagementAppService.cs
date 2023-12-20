using System;
using System.Collections.Generic;
using System.Text;
using HospitalManagement.Localization;
using Volo.Abp.Application.Services;

namespace HospitalManagement;

/* Inherit your application services from this class.
 */
public abstract class HospitalManagementAppService : ApplicationService
{
    protected HospitalManagementAppService()
    {
        LocalizationResource = typeof(HospitalManagementResource);
    }
}
