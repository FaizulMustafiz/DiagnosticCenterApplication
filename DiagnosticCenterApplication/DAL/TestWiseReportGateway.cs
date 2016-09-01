using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.DAL
{
    public class TestWiseReportGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        internal List<DateWiseTestReport> GetDateWiseTestReport(string startDate, string endDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT TestName, SUM(TestCount) as TestCount,SUM(TotalFee) as TotalFee  from DateWiseTestReport WHERE RequestDate BETWEEN '" + startDate + "' AND '" + endDate + "' group by TestName";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTestReport> testWiseReportList = new List<DateWiseTestReport>();

            while (reader.Read())
            {
                DateWiseTestReport testReport = new DateWiseTestReport();

                testReport.TestName = reader["TestName"].ToString();
                testReport.TotalTest = Convert.ToInt32(reader["TestCount"].ToString());
                testReport.TotalFee = Convert.ToInt32(reader["TotalFee"].ToString());

                testWiseReportList.Add(testReport);

            }

            reader.Close();
            connection.Close();
            return testWiseReportList;
        }

        internal List<DateWiseTestReport> NotGetDateWiseTestReport()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"select TestId,TestName from Tests where TestId not in (select t.TestId from Tests t,PatientTests pt where  t.TestId=pt.TestId)";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            List<DateWiseTestReport> testWiseReportList = new List<DateWiseTestReport>();


            while (reader.Read())
            {
                DateWiseTestReport testReport = new DateWiseTestReport();

                testReport.TestName = reader["TestName"].ToString();
                testReport.TotalTest = 0;
                testReport.TotalFee = 0;

                testWiseReportList.Add(testReport);
            }

            reader.Close();
            connection.Close();
            return testWiseReportList;
        }
    }
}