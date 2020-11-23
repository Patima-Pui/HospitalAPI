using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IUserRepository
    {
        string SelectDataIntoDB();
        UserModelList SelectUsersAll();
        UserModelList QueryUsers(UserModelRequest requestSerach);
        string SelectDataFromUsernamePassword(RequestLogin item);
        int InsertDataForRegister(UserProfileModel item);
        DropdownDepartmentListModel SelectDepaertmentFromDB();
    }

    public class UserRepository : IUserRepository
    {
        public string SelectDataIntoDB()
        {
            // var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT * FROM UserTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            string output = "";

            while (rdr.Read())
            {
                output += string.Format("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetString(2));
            }

            return output;
        }

        public string SelectDataFromUsernamePassword(RequestLogin item)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string query = string.Format("SELECT Id FROM UserTbl WHERE Username = '{0}' AND Password = '{1}';", item.Username, item.Password);
            using var cmd = new SqlCommand(query, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            var output = "";

            while (rdr.Read())
            {
                output += string.Format("{0}", rdr.GetInt32(0));
            }

            return output;
        }

        public UserModelList SelectUsersAll()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT Id, Username, Name, Surname, CreateDate FROM UserTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            // string output = "";
            UserModelList output = new UserModelList();
            output.Usertable = new List<UserModel>();

            while (rdr.Read())

            {
                output.Usertable.Add(
                    new UserModel()
                    {
                        id = rdr.GetInt32(0),
                        username = rdr.GetString(1),
                        name = rdr.GetString(2),
                        surname = rdr.GetString(3),
                        createdate = rdr.GetDateTime(4),
                    }
                );
            }

            return output;
        }

        public UserModelList QueryUsers(UserModelRequest requestSerach)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

             string sql = string.Format(@"SELECT Id, Username, Name, Surname, CreateDate FROM UserTbl WHERE (Name LIKE '%{0}%' OR Surname LIKE '%{0}%' OR Username LIKE '%{0}%')
                        ", requestSerach.SearchText);
            
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            UserModelList output = new UserModelList();
            output.Usertable = new List<UserModel>();

            while (rdr.Read())

            {
                output.Usertable.Add(
                     new UserModel()
                     {
                        id = rdr.GetInt32(0),
                        username = rdr.GetString(1),
                        name = rdr.GetString(2),
                        surname = rdr.GetString(3),
                        createdate = rdr.GetDateTime(4),
                     }
                 );
            }
            return output;
        }

        public int InsertDataForRegister(UserProfileModel item)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            DateTime dateTimeVariable = DateTime.Now;
            SqlCommand cmd = new SqlCommand(@"INSERT INTO UserTbl (
                    Username, Password, Name, Surname, Telephone, Email, CreateDate, CreateName, UpdateDate, UpdateName,  DepartmentId
                ) VALUES (
                    @Username, @Password, @Name, @Surname, @Telephone, @Email, @CreateDate, @CreateName, @UpdateDate, @UpdateName, @DepartmentId
                )", con);
            cmd.Parameters.AddWithValue("@Username", item.username);
            cmd.Parameters.AddWithValue("@Password", item.password);
            cmd.Parameters.AddWithValue("@Name", item.name);
            cmd.Parameters.AddWithValue("@Surname", item.surname);
            cmd.Parameters.AddWithValue("@Telephone", item.telephone);
            cmd.Parameters.AddWithValue("@Email", item.email);
            cmd.Parameters.AddWithValue("@CreateDate", dateTimeVariable);
            cmd.Parameters.AddWithValue("@CreateName", item.username);
            cmd.Parameters.AddWithValue("@UpdateDate", dateTimeVariable);
            cmd.Parameters.AddWithValue("@UpdateName", item.username);
            cmd.Parameters.AddWithValue("@DepartmentId", item.departmentId);

            var res = cmd.ExecuteNonQuery();
            return res;
        }

        public DropdownDepartmentListModel SelectDepaertmentFromDB()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT Id, DepartmentName FROM UserDepartment";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            DropdownDepartmentListModel output = new DropdownDepartmentListModel();
            output.departmentList = new List<DropdownDepartmentModel>();

            while (rdr.Read())
            {
                output.departmentList.Add(
                    new DropdownDepartmentModel()
                    {
                        id = rdr.GetInt32(0),
                        name = rdr.GetString(1)
                    }
                );
            };

            return output;

        }
    }

}