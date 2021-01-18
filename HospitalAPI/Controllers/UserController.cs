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
        private readonly ILogService _logService;

        public UserController(IUserService userService, ILogService logService)
        {
            _userService = userService;
            _logService = logService;
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
            if (result.success) // if (result.success == true)
            {
                LogModel logmodel = new LogModel()
                {
                    Action = "Register",
                    TargetName = item.username,
                    CreateName = item.upSertName
                };
                _logService.CreateLog(logmodel);

            }
            return result;
        }

        [HttpPost]
        [Route("AddUser")]
        public ResposeModel AddUser([FromBody] UserProfileModel item)
        {
            ResposeModel result = _userService.UserProfile(item);

            LogModel logmodel = new LogModel()
            {
                Action = "Add",
                TargetName = item.username,
                CreateName = item.upSertName
            };
            _logService.CreateLog(logmodel);

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
            LogModel logmodel = new LogModel()
            {
                Action = "Edit",
                TargetName = request.username,
                CreateName = request.upSertName
            };
            _logService.CreateLog(logmodel);
            return result;
        }

        [HttpDelete]
        [Route("DeleteProfile")]
        public ResposeModel DeleteProfile([FromBody] RequestDeleteModel requestDelete)
        {
            ResposeModel result = _userService.DeleteProfile(requestDelete);

            LogModel logmodel = new LogModel()
            {
                Action = "Delete",
                TargetName = requestDelete.Username,
                CreateName = requestDelete.DeleteName
            };
            _logService.CreateLog(logmodel);

            return result;
        }

    }
}