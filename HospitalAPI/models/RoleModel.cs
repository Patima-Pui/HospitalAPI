using System.Collections.Generic;

namespace HospitalAPI
{
    public class RoleModel
    {
        public int id { get; set; }
        public string role { get; set; }
    }

      public class RoleModelList
    {
        public List<RoleModel> Roletable { get; set; }
    }
}