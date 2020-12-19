using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalAPI.Repositorys
{
    public interface IRolesRepository
    {
        RoleModelList SelectRolesAll();
        PermissionModelList SelectPermissionAll();
        PermissionByIdModelList SelectPermissionsByRoleId(RoleByIdModel requestId);
    }

    public class RolesRepository : IRolesRepository
    {
        public RoleModelList SelectRolesAll()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT Id, Role FROM RoleTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            RoleModelList output = new RoleModelList();
            output.Roletable = new List<RoleModel>();

            while (rdr.Read())

            {
                output.Roletable.Add(
                    new RoleModel()
                    {
                        id = rdr.GetInt32(0),
                        role = rdr.GetString(1),
                    }
                );
            }
            return output;
        }

        public PermissionModelList SelectPermissionAll()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT Id, Permission FROM PermissionTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            PermissionModelList output = new PermissionModelList();
            output.Permissiontable = new List<PermissionModel>();

            while (rdr.Read())

            {
                output.Permissiontable.Add(
                    new PermissionModel()
                    {
                        id = rdr.GetInt32(0),
                        permission = rdr.GetString(1)
                    }
                );
            }
            return output;
        }

        public PermissionByIdModelList SelectPermissionsByRoleId(RoleByIdModel requestId)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs);
            con.Open();

            string sql = string.Format(@"SELECT PermissionId FROM RolePermissionTbl 
                        WHERE RoleId  = {0}", requestId.roleId);

            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            PermissionByIdModelList output = new PermissionByIdModelList();
            output.PermissionIdList = new List<int>();

            while (rdr.Read())

            {
                output.PermissionIdList.Add(
                    rdr.GetInt32(0)
                );
            }
            return output;
        }
    }
}