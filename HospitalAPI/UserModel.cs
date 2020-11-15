using System;
using System.Collections.Generic;

namespace HospitalAPI 
{
    public class UserModel
    {
        public string id {get; set;}
        public string username {get; set;}
        public string name {get; set;}
        public string surname {get; set;}
        public DateTime date {get; set;}

    }

    public class UserModelList {
        public List<UserModel> Datatable {get; set;}
    }

    public class ResposeModel {
        public bool Success { get; set; }
    }

    public class RequestLogin {
        public string Username { get; set; }
        public string Password { get; set; }
    }


}