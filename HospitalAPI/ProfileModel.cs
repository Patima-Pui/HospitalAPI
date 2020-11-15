using System;
using System.Collections.Generic;

namespace HospitalAPI
{
    public class ProfileModel
    {
        public string name {get; set;}
        public string surname {get; set;}
    }

    public class ProfileModelList {
        public List<ProfileModel> Datatable {get; set;}
    }


}
