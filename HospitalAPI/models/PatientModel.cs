using System;
using System.Collections.Generic;

namespace HospitalAPI 
{
    public class PatientModel
    {
        public int hn {get; set;}
        public string name {get; set;}
        public string surname {get; set;}
        public int age {get; set;}
        public DateTime birthday {get; set;}
        public int typeId {get; set;}
        public string typeName {get; set;}
        public int visit {get; set;}
    }

    public class PatientModelList {
        public List<PatientModel> Patienttable {get; set;}
    }

    public class PatientModelRequest {
        public string SearchText { get; set; }
        public int TypeId { get; set; }
    }

}
