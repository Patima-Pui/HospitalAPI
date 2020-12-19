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
            PermissionByIdModelList result = _rolesRepository.SelectPermissionIdAll(requestId);
            return result;
        }
    }
}
