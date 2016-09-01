using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.DAL
{
    public class TestRequestGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        public int SaveTestRequest(TestRequestEntry testRequest)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO Patient(PatientName,DOB,MobileNo,TotalAmount,DueDate,PaymentStatus) VALUES ('" + testRequest.PatientName + "','" + testRequest.Dob + "','" + testRequest.MobileNo + "','" + testRequest.TotalAmount + "','" + testRequest.DueDate + "', '"+testRequest.PaymentStatus+"')";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            int rowAffacted = command.ExecuteNonQuery();
            connection.Close();
            return rowAffacted;
        }

        public int SavepatientTests(int patientId, int testId, string requestDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO PatientTests(PatientId,TestId,RequestDate) VALUES('" + patientId + "', '" + testId + "', '" + requestDate + "')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public TestRequestEntry GetPatientByMobileNo(string mobileNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Patient WHERE MobileNo = '" + mobileNo + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            TestRequestEntry testRequest = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    testRequest = new TestRequestEntry();

                    testRequest.PatientName = reader["PatientName"].ToString();
                    testRequest.MobileNo = reader["MobileNo"].ToString();
                    testRequest.BillNo = reader["BillNo"].ToString();
                    testRequest.PatientId = Convert.ToInt32(reader["PatientId"].ToString());

                }
                reader.Close();
            }
            connection.Close();
            return testRequest;
        }

        public bool IsPatientTestExists(int patientId, int testId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM PatientTests WHERE PatientId= '"+patientId+"'  AND TestId= '"+testId+"'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isPatientTestExist = false;
            if (reader.HasRows)
            {
                isPatientTestExist = true;
            }
            reader.Close();
            connection.Close();
            return isPatientTestExist;
        }

        public bool IsTestIdExists(int testId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Tests WHERE TestId= '" + testId + "'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isTestIdExist = false;
            if (reader.HasRows)
            {
                isTestIdExist = true;
            }
            reader.Close();
            connection.Close();
            return isTestIdExist;
        }


        public bool IsMobileNoxists(string mobileNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Patient Where MobileNo = '" + mobileNo + "'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isMobilenoExists = false;
            if (reader.HasRows)
            {
                isMobilenoExists = true;
            }
            reader.Close();
            connection.Close();
            return isMobilenoExists;
        }

        public TestRequestEntry SearchByBillorMobileNo(string billNo, string mobileNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Patient WHERE BillNo = '" + billNo + "' OR MobileNo = '" + mobileNo + "'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            TestRequestEntry testRequest = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    testRequest = new TestRequestEntry();

                    testRequest.PatientId = Convert.ToInt32(reader["PatientId"].ToString());
                    testRequest.PatientName = reader["PatientName"].ToString();
                    testRequest.MobileNo = reader["MobileNo"].ToString();
                    testRequest.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());
                    testRequest.DueDate = Convert.ToDateTime(reader["DueDate"].ToString());
                    testRequest.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());

                }
                reader.Close();
            }
            connection.Close();
            return testRequest;
        }

        public List<SearchView> GetAllBillInfo(string billNo, string mobileNo)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM SearchView WHERE BillNo = '"+billNo+"' OR MobileNo = '"+mobileNo+"'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<SearchView> searches = new List<SearchView>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    SearchView search = new SearchView();
                    search.PatientId = Convert.ToInt32(reader["PatientId"].ToString());
                    search.BillNo = reader["BillNo"].ToString();
                    search.MobileNo = reader["MobileNo"].ToString();
                    search.TestName = reader["TestName"].ToString();
                    search.TestFee =Convert.ToDecimal(reader["TestFee"].ToString());
                    search.Amount = Convert.ToDecimal(reader["PaymentStatus"].ToString());
                    searches.Add(search);
                }
                reader.Close();
            }
            connection.Close();
            return searches;
        }

        public int UpdatePaymentStatus(string billNo, string mobileNo, decimal amount)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "UPDATE Patient SET PaymentStatus='"+amount+"' WHERE BillNo = '" + billNo + "' OR MobileNo = '" + mobileNo + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }



    }
}