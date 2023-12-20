using HospitalManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HospitalManagement.Permissions;

public class HospitalManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var hospitalManagementGroup = context.AddGroup(HospitalManagementPermissions.GroupName, L("Permission:HospitalManagement"));

        var hospitalsPermission = hospitalManagementGroup.AddPermission(HospitalManagementPermissions.Hospitals.Default, L("Permission:Hospitals"));
        hospitalsPermission.AddChild(HospitalManagementPermissions.Hospitals.Create, L("Permission:Hospitals.Create"));
        hospitalsPermission.AddChild(HospitalManagementPermissions.Hospitals.Edit, L("Permission:Hospitals.Edit"));
        hospitalsPermission.AddChild(HospitalManagementPermissions.Hospitals.Delete, L("Permission:Hospitals.Delete"));

       


    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HospitalManagementResource>(name);
    }
}
