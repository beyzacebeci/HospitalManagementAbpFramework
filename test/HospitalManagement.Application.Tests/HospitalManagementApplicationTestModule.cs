using Volo.Abp.Modularity;

namespace HospitalManagement;

[DependsOn(
    typeof(HospitalManagementApplicationModule),
    typeof(HospitalManagementDomainTestModule)
    )]
public class HospitalManagementApplicationTestModule : AbpModule
{

}
