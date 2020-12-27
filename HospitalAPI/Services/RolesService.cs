using System;
using System.Collections.Generic;
using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IRolesService
    {
        RoleModelList SelectRoles();
        PermissionModelList SelectPermissions();
        PermissionByIdModelList SelectPermissionsById(RoleByIdModel requestId);
        RoleResponseModel InsertRoleService(InsertRoleModel request);
        RoleResponseModel UpdateRoleService(UpdateRoleModel request);
        RoleResponseModel DeleteRoleService(RoleByIdModel requestId);
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
            result.PermissionIdList = new List<PermissionModel>();

            foreach (PermissionModel item in permission.Permissiontable)
            {
                result.PermissionIdList.Add(
                    new PermissionModel()
                    {
                        permissionId = item.permissionId,   //PermisdionTbl
                        permissionName = item.permissionName,   //PermisdionTbl    
                        permissionCheck = rolePermission.IndexOf(item.permissionId) >= 0 ? true : false
                        //Search PermissionId(PermissionTbl) on RolePermisdion(RolePermissionTbl)
                    }
                );
            }

            return result;
        }

        public RoleResponseModel InsertRoleService(InsertRoleModel request)
        {
            RoleResponseModel response = new RoleResponseModel();
            var result = _rolesRepository.NewRoleAttribute(request);
            if (result)
            {
                response.success = true;
            }
            else
            {
                response.success = false;
            }
            return response;
        }

        public RoleResponseModel UpdateRoleService(UpdateRoleModel request)
        {
            RoleResponseModel response = new RoleResponseModel();
            var resDelete = _rolesRepository.DeleteRolePermission(request.roleId);

            if (resDelete > 0)
            {
                var resUpdate = 0;
                resUpdate = _rolesRepository.InsertRolePermission(request.roleId, request.permissionList);
                if (resUpdate > 0)
                {
                    response.success = true;
                }
                else
                {
                    response.success = false;
                }
            }
            else
            {
                response.success = false;
            }

            return response;
        }

        public RoleResponseModel DeleteRoleService(RoleByIdModel requestId)
        {
            try
            {
                RoleResponseModel response = new RoleResponseModel();
                var resRolePermission = _rolesRepository.DeleteRolePermission(requestId.roleId);
                if (resRolePermission > 0)
                {
                    var resRole = _rolesRepository.DeleteRole(requestId.roleId);
                    if (resRole > 0)
                    {
                        response.success = true;
                    }
                    else
                    {
                        response.success = false;
                    }
                }
                else
                {
                    response.success = false;
                }
                return response;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                return new RoleResponseModel() {success = false};
            }
        }
    }
}
