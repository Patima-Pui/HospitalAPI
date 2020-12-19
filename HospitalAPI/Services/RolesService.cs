using System.Collections.Generic;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IRolesService
    {
        RoleModelList SelectRoles();
        PermissionModelList SelectPermissions();
        PermissionByIdModelList SelectPermissionsById(RoleByIdModel requestId);
    }

    public class RolesService : IRolesService
    {
        private IRolesRepository _rolesRepository;

        //Create constructor
        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        //Create Method
        public RoleModelList SelectRoles()
        {
            RoleModelList result = _rolesRepository.SelectRolesAll();
            return result;
        }

        public PermissionModelList SelectPermissions()
        {
            PermissionModelList result = _rolesRepository.SelectPermissionAll();
            return result;
        }

        public PermissionByIdModelList SelectPermissionsById(RoleByIdModel requestId)
        {
            List<int> rolePermission = _rolesRepository.SelectRolePermissionByRoleId(requestId);
            PermissionModelList permission = _rolesRepository.SelectPermissionAll();

            PermissionByIdModelList result = new PermissionByIdModelList();
            result.PermissionIdList = new List<PermissionByIdModel>();

            foreach(PermissionModel item in permission.Permissiontable) {
                result.PermissionIdList.Add(
                    new PermissionByIdModel() {
                        permissionId = item.permissionId,   //PermisdionTbl
                        permissionName = item.permission,   //PermisdionTbl    
                        permissionCheck = rolePermission.IndexOf(item.permissionId) >= 0 ? true : false     
                        //Search PermissionId(PermissionTbl) on RolePermisdion(RolePermissionTbl)
                    }
                );
            }

            return result;
        }
    }
}
