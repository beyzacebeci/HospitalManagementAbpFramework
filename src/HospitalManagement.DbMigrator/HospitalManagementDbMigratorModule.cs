using HospitalManagement.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace HospitalManagement.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(HospitalManagementEntityFrameworkCoreModule),
    typeof(HospitalManagementApplicationContractsModule)
    )]
public class HospitalManagementDbMigratorModule : AbpModule
{
}
