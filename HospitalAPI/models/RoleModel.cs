using System.Collections.Generic;

namespace HospitalAPI
{
    public class RoleModel
    {
        public int id { get; set; }
        public string role { get; set; }
    }

    public class RoleModelList
    {
        public List<RoleModel> Roletable { get; set; }
    }

    public class PermissionModel
    {
        public int id { get; set; }
        public string permission { get; set; }
    }

    public class PermissionModelList
    {
        public List<PermissionModel> Permissiontable { get; set; }
    }

    public class RoleByIdModel
    {
        public int roleId { get; set; }
    }
    public class PermissionByIdModel
    {
        public int permissionId { get; set; }
    }

    public class PermissionByIdModelList
    {
        public List<int> PermissionIdList { get; set; }
    }
}