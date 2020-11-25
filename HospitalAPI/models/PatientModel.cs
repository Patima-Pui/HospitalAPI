using System;
using System.Collections.Generic;

namespace HospitalAPI
{
    public class PatientModel
    {
        public int hn { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int age { get; set; }
        public DateTime birthday { get; set; }
        public int typeId { get; set; }
        public string typeName { get; set; }
        public int visit { get; set; }
        public DateTime appointment { get; set; }
        public string doctor { get; set; }
    }

    public class PatientModelList
    {
        public List<PatientModel> Patienttable { get; set; }
    }

    public class PatientModelRequest
    {
        public string SearchText { get; set; }
        public int TypeId { get; set; }
    }

    public class PatientRequestIdModel
    {
        public int Id { get; set; }
    }

    public class DropdownTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class DropdownTypeModelList
    {
        public List<DropdownTypeModel> typeList { get; set; }
    }
}
