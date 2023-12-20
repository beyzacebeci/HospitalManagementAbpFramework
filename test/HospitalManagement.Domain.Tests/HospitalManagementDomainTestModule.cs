using HospitalManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace HospitalManagement;

[DependsOn(
    typeof(HospitalManagementEntityFrameworkCoreTestModule)
    )]
public class HospitalManagementDomainTestModule : AbpModule
{

}
