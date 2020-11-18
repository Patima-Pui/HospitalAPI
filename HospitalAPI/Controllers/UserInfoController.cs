using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HospitalAPI.Services;

namespace HospitalAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserInfoController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public UserModel Get()
        {
            var result = new UserModel();
            result.id = "00001";
            result.username = "Noom10100";
            result.name = "กิตติศักดิ์";
            result.surname = "จิตรเพียร";
            result.date = new DateTime(1995, 10, 24, 0, 0, 0);

            return result;
        }
        
        [HttpGet]
        [Route("GetList")]
        public UserModelList GetList()
        {
            var result = new UserModelList();
            var item1 = new UserModel();
            item1.id = "00001";
            item1.username = "Noom101";
            item1.name = "กิตติศักดิ์";
            item1.surname = "จิตรเพียร";
            item1.date = new DateTime(1995, 10, 04, 0, 0, 0);
            var item2 = new UserModel();
            item2.id = "00002";
            item2.username = "Stamp05";
            item2.name = "พัสกร";
            item2.surname = "สังสัมฤทธิ์";
            item2.date = new DateTime(1995, 10, 11, 0, 0, 0);
            var item3 = new UserModel();
            item3.id = "00003";
            item3.username = "Babie_hc";
            item3.name = "ปนัดดา";
            item3.surname = "ฮ่อคำ";
            item3.date = new DateTime(1995, 10, 12, 0, 0, 0);
            var item4 = new UserModel();
            item4.id = "00004";
            item4.username = "Boy2543";
            item4.name = "บอย";
            item4.surname = "ตั้งใจเรียน";
            item4.date = new DateTime(1995, 10, 03, 0, 0, 0);
            var item5 = new UserModel();
            item5.id = "00005";
            item5.username = "Man_sc";
            item5.name = "สมชาย";
            item5.surname = "ตั้แมนมนุษย์";
            item5.date = new DateTime(1995, 10, 25, 0, 0, 0);
            var item6 = new UserModel();
            item6.id = "00006";
            item6.username = "Admin";
            item6.name = "ภาณุพงษ์";
            item6.surname = "แจ่มแจ้ง";
            item6.date = new DateTime(1995, 10, 25, 0, 0, 0);

            result.Datatable = new List<UserModel>();
            result.Datatable.Add(item1);
            result.Datatable.Add(item2);
            result.Datatable.Add(item3);
            result.Datatable.Add(item4);
            result.Datatable.Add(item5);
            result.Datatable.Add(item6);

            return result;
        }

        [HttpGet]
        [Route("GetArray")]
        public UserModel[] GetArray()
        {
            UserModel[] p = new UserModel[3];
            p[0] = new UserModel();
            p[0].id = "00001";
            p[0].username = "Noom101";
            p[0].name = "กิตติศักดิ์";
            p[0].surname = "จิตรเพียร";
            p[0].date = new DateTime(1995, 10, 04, 0, 0, 0);
            p[1] = new UserModel();
            p[1].id = "00002";
            p[1].username = "Stamp05";
            p[1].name = "พัสกร";
            p[1].surname = "สังสัมฤทธิ์";
            p[1].date = new DateTime(1995, 10, 11, 0, 0, 0);
            p[2] = new UserModel();
            p[2].id = "00003";
            p[2].username = "Babie_hc";
            p[2].name = "ปนัดดา";
            p[2].surname = "ฮ่อคำ";
            p[2].date = new DateTime(1995, 10, 12, 0, 0, 0);
            return p;
        }

        [HttpGet]
        [Route("TestConnectionDatabase")]
        public string TestConnectionDatabase()
        {
            var result = _userService.SelectData();
            return result;
        }
    }
}