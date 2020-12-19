using HospitalAPI.Repositorys;

namespace HospitalAPI.Services
{
    public interface IRolesService
    {
        RoleModelList SelectRoles();
        PermissionModelList SelectPermissions();
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
            PermissionModelList result = _rolesRepository.SelectPermissiomAll();
            return result;
        }
    }
}
