using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalAPI.Repositorys
{
    public interface IRolesRepository
    {
        RoleModelList SelectRolesAll();
        PermissionModelList SelectPermissionAll();
        List<int> SelectRolePermissionByRoleId(RoleByIdModel requestId);
        bool NewRoleAttribute(InsertRoleModel request);
        int InsertRolePermission(int roleId, List<PermissionModel> request);
        int DeleteRole(int roleId);
        int DeleteRolePermission(int roleId);

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
                        permissionId = rdr.GetInt32(0),
                        permissionName = rdr.GetString(1),
                        permissionCheck = false
                    }
                );
            }
            return output;
        }

        public List<int> SelectRolePermissionByRoleId(RoleByIdModel requestId)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs);
            con.Open();

            string sql = string.Format(@"SELECT PermissionId FROM RolePermissionTbl 
                        WHERE RoleId  = {0}", requestId.roleId);

            using var cmd = new SqlCommand(sql, con);
            using SqlDataReader rdr = cmd.ExecuteReader();

            List<int> output = new List<int>();

            while (rdr.Read())
            {
                output.Add(
                    rdr.GetInt32(0)
                );
            }
            return output;
        }

        public bool NewRoleAttribute(InsertRoleModel request)
        {
            try
            {
                int resRole = InsertRole(request.roleName, request.username);
                int roleId = SelectRoleId(request.roleName);
                int resRolePermisison = 0;

                if (roleId >= 0)
                {
                    resRolePermisison = InsertRolePermission(roleId, request.permissionList);
                }

                if (resRole == 1 && resRolePermisison == 0)
                {
                    DeleteRole(roleId);
                    return false;
                }
                else if (resRole == 1 && resRolePermisison >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                return false;
            }
        }

        public int InsertRole(string roleName, string username)
        {
            try
            {
                var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
                using var con = new SqlConnection(cs);
                con.Open();

                DateTime dateTimeVariable = DateTime.Now;

                using var cmd = new SqlCommand(@"INSERT INTO RoleTbl (Role, CreateDate, CreateName, UpdateDate, UpdateName) 
                            VALUES (@Role, @CreateDate, @CreateName, @UpdateDate, @UpdateName);", con);
                cmd.Parameters.AddWithValue("@Role", roleName);
                cmd.Parameters.AddWithValue("@CreateDate", dateTimeVariable);
                cmd.Parameters.AddWithValue("@CreateName", username);
                cmd.Parameters.AddWithValue("@UpdateDate", dateTimeVariable);
                cmd.Parameters.AddWithValue("@UpdateName", username);

                var resRole = cmd.ExecuteNonQuery();
                return resRole;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                throw;
            }
        }

        public int SelectRoleId(string roleName)
        {
            try
            {
                var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
                using var con = new SqlConnection(cs);
                con.Open();

                string sql = string.Format(@"SELECT Id FROM RoleTbl WHERE Role = '{0}';", roleName);
                using var cmd = new SqlCommand(sql, con);

                using SqlDataReader rdr = cmd.ExecuteReader();

                int roleId = -1;
                while (rdr.Read())
                {
                    roleId = rdr.GetInt32(0);
                }
                return roleId;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                throw;
            }
        }

        public int InsertRolePermission(int roleId, List<PermissionModel> request)
        {
            try
            {
                var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
                using var con = new SqlConnection(cs);
                con.Open();

                string sqlValueInsert = "";

                //Permission on this role
                foreach (PermissionModel item in request)
                {
                    if (item.permissionCheck)
                    {
                        sqlValueInsert += string.Format("({0}, {1}),", roleId, item.permissionId);
                    }
                }
                sqlValueInsert = sqlValueInsert.Remove(sqlValueInsert.Length - 1);

                //Insert Permissiomns into RolePermisisonTbl 
                using var cmd = new SqlCommand(string.Format("INSERT INTO RolePermissionTbl (RoleId, PermissionId) VALUES {0};", sqlValueInsert), con);

                var resRolePermisison = cmd.ExecuteNonQuery();
                return resRolePermisison;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                return 0;
            }
        }

        public int DeleteRole(int roleId)
        {
            try
            {
                var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
                using var con = new SqlConnection(cs);
                con.Open();

                string sql = string.Format(@"DELETE FROM RoleTbl WHERE Id = {0}", roleId);

                using var cmd = new SqlCommand(sql, con);

                var res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                throw;
            }
        }

        public int DeleteRolePermission(int roleId)
        {
            try
            {
                var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
                using var con = new SqlConnection(cs);
                con.Open();

                string sql = string.Format(@"DELETE FROM RolePermissionTbl WHERE RoleId = {0}", roleId);

                using var cmd = new SqlCommand(sql, con);

                var res = cmd.ExecuteNonQuery();
                return res;
            }
            catch (Exception error)
            {
                Console.WriteLine("Error: " + error);
                return 0;
            }
        }
    }
}