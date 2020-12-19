using HospitalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        [Route("RolesAll")]
        public RoleModelList GetRoleList()
        {
           RoleModelList result = _rolesService.SelectRoles();
           return result;
        }

        [HttpGet]
        [Route("PermissionAll")]
        public PermissionModelList GetPermissionList()
        {
            PermissionModelList result = _rolesService.SelectPermissions();
            return result;
        }

        [HttpGet]
        [Route("PermissionById")]
        public PermissionByIdModelList GetPermissionListById([FromQuery] RoleByIdModel requestId)
        {
            PermissionByIdModelList result = _rolesService.SelectPermissionsById(requestId);
            return result;
        }
    }
}