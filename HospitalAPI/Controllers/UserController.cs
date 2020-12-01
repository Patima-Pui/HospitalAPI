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
        public ResposeLoginModel Login([FromBody] RequestLogin item)
        {
            ResposeLoginModel result = _userService.ValidateLogin(item);
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
        [Route("UserAll")]
        public UserModelList GetUserList()
        {
            UserModelList result = _userService.SelectUsers();
            return result;
        }

        [HttpGet]
        [Route("QueryUser")]
        public UserModelList GetQueryUserList([FromQuery] UserModelRequest request)
        {
            UserModelList result = _userService.SelectUserList(request);
            return result;
        }

        [HttpGet]
        [Route("UserInfo")]
        public UserProfileModel GetIndividualPatient([FromQuery] UserRequestIdModel requestId)
        {
            UserProfileModel result = _userService.SelectIndividual(requestId);
            return result;
        }

        [HttpGet]
        [Route("DropdownDepartment")]
        public DropdownDepartmentListModel GetDepartmentList()
        {
            DropdownDepartmentListModel result = _userService.DepartmentList();
            return result;
        }

        [HttpPut]
        [Route("UpdateUserProfile")]
        public ResposeModel UpdateUserProfile([FromBody] UserProfileModel request)
        {
            ResposeModel result = _userService.UpdateUserProfile(request);
            return result;
        }

        [HttpDelete]
        [Route("DeleteProfile")]
        public ResposeModel DeleteProfile([FromBody] UserRequestIdModel requestId)
        {
            ResposeModel result = _userService.DeleteProfile(requestId);
            return result; 
        }
    }
}