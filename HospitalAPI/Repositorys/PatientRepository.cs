using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HospitalAPI.Repositorys
{
    public interface IPatientRepository
    {
        PatientModelList QueryPatients(PatientModelRequest requestSerach);
        PatientModel SelectIndividualRepo(PatientRequestIdModel requestId);
        DropdownTypeModelList SelectTypeListFromDB();
    }

    public class PatientRepository : IPatientRepository
    {
        public PatientModelList QueryPatients(PatientModelRequest requestSerach)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sqlTypeId = ";";
            if (requestSerach.TypeId != -1)
            {
                sqlTypeId = string.Format("AND a.[TypeId] = {0};", requestSerach.TypeId);
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
                        ", requestSerach.SearchText) + sqlTypeId;
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            // string output = "";
            PatientModelList output = new PatientModelList();
            output.Patienttable = new List<PatientModel>();

            while (rdr.Read())

            {
                output.Patienttable.Add(
                     new PatientModel()
                     {
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

        public PatientModel SelectIndividualRepo(PatientRequestIdModel requestId)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = string.Format(@"SELECT   a.Id
                                    ,a.Name
                                    ,a.Surname
                                    ,a.Age
                                    ,a.BirthDay
                                    ,a.TypeId
                                    ,a.NoOfVisit
                                    ,a.AppointmentDate
                                    ,a.Doctor
                                    ,b.TypeName
                        FROM PatientTbl a
                        LEFT JOIN PatientType b
                        ON a.TypeId = b.Id
                        WHERE a.[Id] = {0};
                        ", requestId.Id);
            using var cmd = new SqlCommand(sql, con); //Using Class SqlCommand for query data

            using SqlDataReader rdr = cmd.ExecuteReader();

            PatientModel output = new PatientModel();

            while (rdr.Read())

            {
                {
                    output.hn = rdr.GetInt32(0);
                    output.name = rdr.GetString(1);
                    output.surname = rdr.GetString(2);
                    output.age = rdr.GetInt32(3);
                    output.birthday = rdr.GetDateTime(4);
                    output.typeId = rdr.GetInt32(5);
                    output.visit = rdr.GetInt32(6);
                    output.appointment = rdr.GetDateTime(7);
                    output.doctor = rdr.GetString(8);
                    output.typeName = rdr.GetString(9);
                };
            }
            return output;
        }

        public DropdownTypeModelList SelectTypeListFromDB()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            string sql = "SELECT Id, TypeName FROM PatientType";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            DropdownTypeModelList output = new DropdownTypeModelList();
            output.typeList = new List<DropdownTypeModel>();

            while (rdr.Read())
            {
                output.typeList.Add(
                    new DropdownTypeModel()
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