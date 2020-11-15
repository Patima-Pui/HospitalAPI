using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : ControllerBase 
    {
        [HttpGet]
        public ProfileModel Get() 
        {
            var result = new ProfileModel();
                result.name = "pui";
                result.surname = "kung";
            return result;
        }

        [HttpGet]
        [Route("GetList")]
        public ProfileModelList GetList() {

            var result = new ProfileModelList();

            var item1 = new ProfileModel();
                item1.name = "number one";
                item1.surname = "surname number one";
            var item2 = new ProfileModel();
                item2.name = "number two";
                item2.surname = "surname number two";
            var item3 = new ProfileModel();
                item3.name = "number three";
                item3.surname = "surname number three";

            result.Datatable = new List<ProfileModel>();
            result.Datatable.Add(item1);
            result.Datatable.Add(item2);
            result.Datatable.Add(item3);

            return result;
        }
    }
}