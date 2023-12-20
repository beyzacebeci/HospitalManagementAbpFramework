using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HospitalManagement.Web;

[Dependency(ReplaceServices = true)]
public class HospitalManagementBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HospitalManagement";
}
