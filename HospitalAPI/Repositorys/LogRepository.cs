using System;
using System.Data.SqlClient;

namespace HospitalAPI.Repositorys
{
    public interface ILogRepository
    {
        void InsertLog(LogModel request);
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
    }


}