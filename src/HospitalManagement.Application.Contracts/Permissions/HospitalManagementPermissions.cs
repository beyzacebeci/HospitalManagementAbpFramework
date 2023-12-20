namespace HospitalManagement.Permissions;

public static class HospitalManagementPermissions
{
    public const string GroupName = "HospitalManagement";

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
    public static class Hospitals
    {
        public const string Default = GroupName + ".Hospitals";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";


    }
}
