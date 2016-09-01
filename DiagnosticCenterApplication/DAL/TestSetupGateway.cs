using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterApplication.DAL;
using DiagnosticCenterApplication.Models;
using DiagnosticCenterApplication.Models.ViewModels;

namespace DiagnosticCenterApplication.DAL
{
    public class TestSetupGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        public int SaveTest(TestSetupEntry test)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "INSERT INTO Tests VALUES('" + test.TestName + "', " + test.TestFee + ", " + test.TestTypeId + ")";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();

            int rowAffected = command.ExecuteNonQuery();

            connection.Close();
            return rowAffected;
        }


        public bool IsTestNameExist(TestSetupEntry test)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Tests WHERE TestName='" + test.TestName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isTestNameExist = false;
            if (reader.HasRows)
            {
                isTestNameExist = true;
            }
            reader.Close();
            connection.Close();
            return isTestNameExist;
        }
  
        public List<ViewTest> GetAllTests()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM ViewTests ORDER BY TestName ASC";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            List<ViewTest> tests = new List<ViewTest>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ViewTest test = new ViewTest();
                    test.TestName = reader["TestName"].ToString();
                    test.TestFee = Convert.ToDecimal(reader["TestFee"].ToString());
                    test.TestTypeName = reader["TestTypeName"].ToString();

                    tests.Add(test);
                }
                reader.Close();
            }
            connection.Close();
            return tests;
        }

        public List<TestSetupEntry> GetAllTestWithType()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Tests ORDER BY TestName ASC";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<TestSetupEntry> tests = new List<TestSetupEntry>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TestSetupEntry test = new TestSetupEntry();
                    test.TestId = Convert.ToInt32(reader["TestId"].ToString());
                    test.TestName = reader["TestName"].ToString();
                    test.TestFee = Convert.ToDecimal(reader["TestFee"].ToString());
                    test.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());

                    tests.Add(test);
                }
                reader.Close();
            }
            connection.Close();
            return tests;
        }

        //public double GetFee(string id)
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);
        //    string query = "SELECT * FROM Tests WHERE TestId = '" + id + "'";

        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();
        //    SqlDataReader reader = command.ExecuteReader();

        //    double fee = 0;
        //    while (reader.Read())
        //    {
        //        fee = Convert.ToDouble(reader["Fee"].ToString());
        //    }
        //    reader.Close();
        //    connection.Close();
        //    return fee;
        //}

        public TestSetupEntry GetTestByTestId(int testId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Tests WHERE TestId = '" + testId + "'";

            SqlCommand command = new SqlCommand(query, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            TestSetupEntry test = null;

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    test = new TestSetupEntry();
                    test.TestId = Convert.ToInt32(reader["TestId"].ToString());
                    test.TestName = reader["TestName"].ToString();
                    test.TestFee = Convert.ToDecimal(reader["TestFee"].ToString());
                    test.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());
                }
                reader.Close();
            }
            connection.Close();
            return test;
        }
    }

}