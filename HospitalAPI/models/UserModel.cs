using System;
using System.Collections.Generic;

namespace HospitalAPI
{
    public class TestModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime date { get; set; }

    }

    public class TestModelList
    {
        public List<TestModel> Datatable { get; set; }
    }
    ////////////////////////////////////////////////////
    public class UserModel
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public DateTime createdate { get; set; }

    }

    public class UserModelList
    {
        public List<UserModel> Usertable { get; set; }
    }

    public class ResposeModel
    {
        public bool success { get; set; }
    }

    public class RequestLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserProfileModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
         public string telephone { get; set; }
         public string email { get; set; }
         public int departmentId { get; set; }
    }

    public class DropdownDepartmentModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class DropdownDepartmentListModel
    {
        public List<DropdownDepartmentModel> departmentList { get; set; }
    }
}
