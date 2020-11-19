using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IPatientRepository
    {
        PatientModelList SelectPatientsAll(PatientModelRequest requestSerach);
    }

    public class PatientRepository : IPatientRepository
    {
        public PatientModelList SelectPatientsAll(PatientModelRequest requestSerach)
        {
            // var cs = "Data Source=192.168.43.180,1433;Initial Catalog= HospitalDB;User Id=sa;Password=reallyStrongPwd123;";
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();
            
            string sqlTyprId = ";";
            if(requestSerach.TypeId != -1) {
                sqlTyprId = string.Format("AND a.[TypeId] = {0};", requestSerach.TypeId);
            }

            string sql = string.Format(@"SELECT   a.Id
                                    ,a.Name
                                    ,a.Surname
                                    ,a.Age
                                    ,a.BirthDay
                                    ,a.TypeId
                                    ,a.NoOfVisit
                                    ,b.TypeName
                        FROM PatientTbl a
                        LEFT JOIN PatientType b
                        ON a.TypeId = b.Id
                        WHERE (a.[Name] LIKE '%{0}%' 
                            OR a.[Surname] LIKE '%{0}%'
                            OR a.[Id] LIKE '%{0}%'
                            OR a.[NoOfVisit] LIKE '%{0}%')
                        ", requestSerach.SearchText) + sqlTyprId;
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
                        hn = rdr.GetInt32(0),
                        name = rdr.GetString(1),
                        surname = rdr.GetString(2),
                        age = rdr.GetInt32(3),
                        birthday = rdr.GetDateTime(4),
                        typeId = rdr.GetInt32(5),
                        visit = rdr.GetInt32(6),
                        typeName = rdr.GetString(7)
                    }
                );
            }

            return output;
        }

    }

}