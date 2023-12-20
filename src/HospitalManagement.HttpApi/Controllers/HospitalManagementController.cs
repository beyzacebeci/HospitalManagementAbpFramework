using HospitalManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HospitalManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HospitalManagementController : AbpControllerBase
{
    protected HospitalManagementController()
    {
        LocalizationResource = typeof(HospitalManagementResource);
    }
}
