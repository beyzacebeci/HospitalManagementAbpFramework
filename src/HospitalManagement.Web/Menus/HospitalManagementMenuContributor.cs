using System.Threading.Tasks;
using HospitalManagement.Localization;
using HospitalManagement.MultiTenancy;
using HospitalManagement.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace HospitalManagement.Web.Menus;

public class HospitalManagementMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<HospitalManagementResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                HospitalManagementMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fas fa-home",
                order: 0
            )
        );
                context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                HospitalManagementMenus.Hospitals,
                l["Menu:Hospital"],
                "/Hospitals",
                icon: "fas fa-hospital",
                order: 1
            ).RequirePermissions(HospitalManagementPermissions.Hospitals.Default)
        );
               context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                HospitalManagementMenus.Doctors,
                l["Menu:Doctor"],
                "/Doctors",
                icon: "fas fa-user-md",
                order: 2
            ).RequirePermissions(HospitalManagementPermissions.Hospitals.Default)
        );

        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}
