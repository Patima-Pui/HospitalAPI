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
        public List<RoleModel> RoleList { get; set; }
    }

    public class PermissionModel
    {
        public int permissionId { get; set; }
        public string permissionName { get; set; }
        public bool permissionCheck { get; set; }
    }

    public class PermissionModelList
    {
        public List<PermissionModel> Permissiontable { get; set; }
    }

    public class PermissionByUserIdModelList
    {
        public string role { get; set; }
        public List<PermissionModel> permissions { get; set; }
    }

    public class RoleByIdModel
    {
        public int roleId { get; set; }
    }

    public class PermissionByIdModelList
    {
        public List<PermissionModel> PermissionIdList { get; set; }
    }

    // public class UpsertRoleModel //Request from frontEnd to backEnd
    // {
    //     public string roleName { get; set; }
    //     public string username { get; set; }
    //     public List<PermissionModel> permissionList { get; set; }
    // }

    public class RequestRoleModel
    {
        public string username { get; set; }
        public List<PermissionModel> permissionList { get; set; }
    }

    public class InsertRoleModel : RequestRoleModel
    {
        public string roleName { get; set; }
    }

    public class UpdateRoleModel : RequestRoleModel
    {
        public int roleId { get; set; }
    }

    public class RoleResponseModel
    {
        public bool success { get; set; }
    }

}