using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading;

namespace HospitalAPI.Repositorys
{
    public interface ILogRepository
    {
        void InsertLog(LogModel request);
        LogModelList SelectLogList(SearchLogModel request);
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
            cmd.Parameters.AddWithValue("@Target", request.TargetName);
            cmd.Parameters.AddWithValue("@CreateName", request.CreateName);
            cmd.Parameters.AddWithValue("@CreateDate", dateTimeVariable);
            cmd.ExecuteNonQuery();
        }

        public LogModelList SelectLogList(SearchLogModel request)
        {
            var cs = "Server=localhost\\SQLEXPRESS;Database=HospitalDB;Trusted_Connection=True;";
            using var con = new SqlConnection(cs);
            con.Open();
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            string defaultQuery = "SELECT Id, Action, TargetName, CreateName, CreateDate FROM LogTbl";

            string ActionQuery = request.Action == null ? "" : string.Format(" AND Action = '{0}'", request.Action);
            string CreateNameQuery = request.CreateName == null ? "" : string.Format(" AND CreateName = '{0}'", request.CreateName);
            string targetNameQuery = request.TargetName == null ? "" : string.Format(" AND TargetName = '{0}'", request.TargetName);

            string sql = string.Format("{0} WHERE CreateDate BETWEEN '{1}' AND '{2}' {3}{4}{5};"
            , defaultQuery
            , request.StartDate.ToString("yyyy-MM-dd HH:mm:ss")
            , request.EndDate.ToString("yyyy-MM-dd HH:mm:ss")
            , ActionQuery
            , CreateNameQuery
            , targetNameQuery
            );

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
                        TargetName = rdr.GetString(2),
                        CreateName = rdr.GetString(3),
                        CreateDate = rdr.GetDateTime(4),
                    }
                );
            }
            return output;
        }

    }

}