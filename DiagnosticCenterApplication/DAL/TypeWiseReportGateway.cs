using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.DAL
{
    public class TypeWiseReportGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        internal List<DateWiseTypeReport> GetDateWiseTestReport(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string qurey = @"SELECT TestTypeName, SUM(TestCount) as TestCount,COALESCE(SUM(TotalFee),0) as TotalFee FROM DateWiseTestTypeReport 
WHERE RequestDate BETWEEN '" + startDate + "' AND '" + endDate + "' group by TestTypeName";

            SqlCommand command = new SqlCommand(qurey, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTypeReport> typeWisReportList = new List<DateWiseTypeReport>();

            while (reader.Read())
            {
                DateWiseTypeReport testReport = new DateWiseTypeReport();

                testReport.TestTypeName = reader["TestTypeName"].ToString();
                testReport.TotalTest = Convert.ToInt32(reader["TestCount"].ToString());
                testReport.TotalFee = Convert.ToInt32(reader["TotalFee"].ToString());

                typeWisReportList.Add(testReport);
            }

            reader.Close();
            connection.Close();
            return typeWisReportList;

        }

        internal List<DateWiseTypeReport> NotGetDateWiseTestreport()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = @"select TestTypeId,TestTypeName from TestType where TestTypeId not in (select tt.TestTypeId from TestType tt,Tests t where  tt.TestTypeId=t.TestTypeId)";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTypeReport> typeWiseReportList = new List<DateWiseTypeReport>();

            while (reader.Read())
            {
                DateWiseTypeReport testReport = new DateWiseTypeReport();

                testReport.TestTypeName = reader["TestTypeName"].ToString();
                testReport.TotalTest = 0;
                testReport.TotalFee = 0;

                typeWiseReportList.Add(testReport);

            }

            reader.Close();
            connection.Close();
            return typeWiseReportList;
        }

    }
}