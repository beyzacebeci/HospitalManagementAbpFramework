using System.Threading.Tasks;

namespace HospitalManagement.Data;

public interface IHospitalManagementDbSchemaMigrator
{
    Task MigrateAsync();
}
