using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HospitalAPI.Services;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Login")]
        public ResposeModel Login([FromBody] RequestLogin item)
        {
            ResposeModel result = _userService.ValidateLogin(item);
            return result;
        }

        [HttpPost]
        [Route("Register")]
        public ResposeModel Register([FromBody] UserProfileModel item)
        {
            ResposeModel result = _userService.UserProfile(item);
            return result;
        }

        [HttpGet]
        [Route("UserInfo")]
        public UserModelList GetUserList()
        {
            UserModelList result = _userService.SelectUsers();
            return result;
        }

        [HttpGet]
        [Route("DropdownDepartment")]
        public DropdownDepartmentListModel GetDepartmentList()
        {
            DropdownDepartmentListModel result = _userService.DepartmentList();
            return result;
        }
    }
}