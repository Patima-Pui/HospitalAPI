using System;
using System.Collections.Generic;
namespace HospitalAPI
{

    public class LogModel
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Target { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class LogModelList
    {
        public List<LogModel> Logtable { get; set; }
    }
}
