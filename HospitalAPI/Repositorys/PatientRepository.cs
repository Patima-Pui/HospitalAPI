using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IPatientRepository
    {
        PatientModelList SelectPatientsAll();
    }

    public class PatientRepository : IPatientRepository
    {
        public PatientModelList SelectPatientsAll()
        {
            // var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT * FROM PatientTbl";
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            // string output = "";
            PatientModelList output = new PatientModelList();
            output.Patienttable = new List<PatientModel>();

            while (rdr.Read())

            {
                //output=HN(0) Name(1) Surname(2) Age(3) Birthday(4) TypeID(5) Visit(6)
                // output += string.Format("{0} {1} {2} {3} {4} {5} {6}", rdr.GetString(0), rdr.GetString(1),
                //         rdr.GetString(2), rdr.GetInt32(3), rdr.GetDateTime(4), rdr.GetString(5), rdr.GetString(6) );
                output.Patienttable.Add(
                    new PatientModel(){
                        number = rdr.GetInt32(0),
                        HN = rdr.GetString(1),
                        name = rdr.GetString(2),
                        surname = rdr.GetString(3),
                        age = rdr.GetInt32(4),
                        birthday = rdr.GetDateTime(5),
                        typeId = rdr.GetInt32(6),
                        visit = rdr.GetInt32(7) 
                    }
                );
            }

            return output;
        }

    }

}