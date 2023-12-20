using HospitalManagement.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HospitalManagement.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class HospitalManagementPageModel : AbpPageModel
{
    protected HospitalManagementPageModel()
    {
        LocalizationResourceType = typeof(HospitalManagementResource);
    }
}
