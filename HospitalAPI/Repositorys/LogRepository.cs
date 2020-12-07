using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalAPI.Repositorys
{
    public interface ILogRepository
    {
        void InsertLog(LogModel request);
        LogModelList SelectLogListAll();
    }

    public class LogRepository : ILogRepository
    {
        public void InsertLog(LogModel request)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs); //Using Class SqlConnection for COnnent to database
            con.Open();

            DateTime dateTimeVariable = DateTime.Now;
            SqlCommand cmd = new SqlCommand(@"INSERT INTO LogTbl (
                    Action, Target, CreateName, CreateDate) VALUES (
                    @Action, @Target, @CreateName, @CreateDate)", con);
            cmd.Parameters.AddWithValue("@Action", request.Action);
            cmd.Parameters.AddWithValue("@Target", request.Target);
            cmd.Parameters.AddWithValue("@CreateName", request.CreateName);
            cmd.Parameters.AddWithValue("@CreateDate", dateTimeVariable);
            cmd.ExecuteNonQuery();
        }

        public LogModelList SelectLogListAll()
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs);
            con.Open();

            string sql = "SELECT Id, Action, Target, CreateName, CreateDate FROM LogTbl";
            using var cmd = new SqlCommand(sql, con);

            using SqlDataReader rdr = cmd.ExecuteReader();

            LogModelList output = new LogModelList();
            output.Logtable = new List<LogModel>();

            while (rdr.Read())
            {
                output.Logtable.Add(
                    new LogModel()
                    {
                        Id = rdr.GetInt32(0),
                        Action = rdr.GetString(1),
                        Target = rdr.GetString(2),
                        CreateName = rdr.GetString(3),
                        CreateDate = rdr.GetDateTime(4),
                    }
                );
            }
            return output;
        }

    }

}