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
        PermissionByUserIdModelList SelectPermisionByUserId(UserRequestIdModel userId);
    }

    public class RolesService : IRolesService
    {
        private IRolesRepository _rolesRepository;
        private IUserRepository _userRepository;

        //Create constructor
        public RolesService(IRolesRepository rolesRepository, IUserRepository userRepository)
        {
            _rolesRepository = rolesRepository;
            _userRepository = userRepository;
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

        public PermissionByUserIdModelList SelectPermisionByUserId(UserRequestIdModel userId)
        {
            // userRepository ดึงข้อมูล role id, role name โดย ส่ง user id ไปหา ต้อง join กับ table role ด้วยนะ
            RoleModel roldInfo = _userRepository.SelectRoleIdByUserId(userId);
            RoleByIdModel roleByIdModel = new RoleByIdModel();
            roleByIdModel.roleId = roldInfo.id;
            // roleByIdModel.roleId = 1;
            // roleRepository ดึงข้อมูล permision โดยส่ง role id
            List<int> rolePermission = _rolesRepository.SelectRolePermissionByRoleId(roleByIdModel);
                        
            PermissionModelList permission = _rolesRepository.SelectPermissionAll();

            PermissionByUserIdModelList result = new PermissionByUserIdModelList();

            // result.role = "Admin";
            result.role = roldInfo.role;
            result.permissions = new List<PermissionModel>();

            foreach (PermissionModel item in permission.Permissiontable)
            {
                result.permissions.Add(
                    new PermissionModel()
                    {
                        permissionId = item.permissionId,   //PermisdionTbl
                        permissionName = item.permissionName,   //PermisdionTbl    
                        permissionCheck = rolePermission.IndexOf(item.permissionId) >= 0 ? true : false
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
                return new RoleResponseModel() { success = false };
            }
        }
    }
}
