using System;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IUserRepository
    {
        string SelectDataIntoDB();
        string SelectDataFromUsernamePassword(RequestLogin item);
    }

    public class UserRepository : IUserRepository
    {
        public string SelectDataIntoDB()
        {
            var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
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
            var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string query = string.Format("SELECT * FROM UserTbl WHERE Username = '{0}' AND Password = '{1}';", item.Username, item.Password);
            using var cmd = new SqlCommand(query, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            var output = "";

            while (rdr.Read())
            {
                output += string.Format("{0}", rdr.GetInt32(0));
            }

            return output;
        }
    }


}