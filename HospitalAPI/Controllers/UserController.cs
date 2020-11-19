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
        [Route("login")]
        public ResposeModel Login([FromBody] RequestLogin item)
        {
            ResposeModel result = _userService.ValidateLogin(item);
            return result;
        }

        [HttpGet]
        [Route("UserInfo")]
        public UserModelList GetUserList()
        {
            UserModelList result = _userService.SelectUsers();
            return result;
        }
    }
}