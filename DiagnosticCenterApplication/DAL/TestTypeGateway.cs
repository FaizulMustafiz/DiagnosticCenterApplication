using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using DiagnosticCenterApplication.Models;

namespace DiagnosticCenterApplication.DAL
{
    public class TestTypeGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;


        public int SaveTestType(TestTypeEntry testType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "INSERT INTO TestType VALUES('" + testType.TestTypeName + "')";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }


        public bool IsTypeNameExist(TestTypeEntry testType)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType WHERE TestTypeName='" + testType.TestTypeName + "'";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            bool isTypeNameExist = false;
            if (reader.HasRows)
            {
                isTypeNameExist = true;
            }
            reader.Close();
            connection.Close();
            return isTypeNameExist;
        }

        public List<TestTypeEntry> GetAllTestTypes()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM TestType ORDER BY TestTypeName ASC";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<TestTypeEntry> testTypes = new List<TestTypeEntry>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    TestTypeEntry testType = new TestTypeEntry();
                    testType.TestTypeId = Convert.ToInt32(reader["TestTypeId"].ToString());
                    testType.TestTypeName = reader["TestTypeName"].ToString();
                    testTypes.Add(testType);
                }
                reader.Close();
            }
            connection.Close();
            return testTypes;
        }
    }
}