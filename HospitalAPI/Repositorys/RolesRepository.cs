using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalAPI.Repositorys
{
    public interface IRolesRepository
    {
        RoleModelList SelectRolesAll();
        PermissionModelList SelectPermissiomAll();
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

        public PermissionModelList SelectPermissiomAll()
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
                        permission = rdr.GetString(1),
                    }
                );
            }
            return output;
        }
    }
}