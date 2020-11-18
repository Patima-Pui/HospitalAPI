using System;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IPatientRepository
    {
        string SelectPatientsAll();
    }

    public class PatientRepository : IPatientRepository
    {
        public string SelectPatientsAll()
        {
            // var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT * FROM PatientTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            string output = "";

            while (rdr.Read())
            {
                //output=HN(0) Name(1) Surname(2) Age(3) Birthday(4) Type(5) Visit(6)
                output += string.Format("{0} {1} {2} {3} {4} {5} {6}", rdr.GetInt32(0), rdr.GetString(1),
                        rdr.GetString(2), rdr.GetInt32(3), rdr.GetDateTime(4), rdr.GetString(5), rdr.GetString(6) );
            }
            return output;
        }

    }

}