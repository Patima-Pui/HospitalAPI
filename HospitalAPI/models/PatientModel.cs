using System;
using System.Collections.Generic;

namespace HospitalAPI 
{
    public class PatientModel
    {
        public int HN {get; set;}
        public string name {get; set;}
        public string surname {get; set;}
        public int age {get; set;}
        public DateTime birthday {get; set;}
        public int typeId {get; set;}
        public int visit {get; set;}
    }

    public class PatientModelList {
        public List<PatientModel> Datatable {get; set;}
    }

}
