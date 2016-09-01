using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Script.Serialization;


namespace DiagnosticCenterApplication
{
    /// <summary>
    /// Summary description for BillNoHandler
    /// </summary>
    public class BillNoHandler : IHttpHandler
    {

        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"] ?? "";
            List<string> billNoList = new List<string>();

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("spGetBillNo", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "@term",
                Value = term
            });

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    billNoList.Add(reader["BillNo"].ToString());
                }
            }

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer(); 
            context.Response.Write(jsSerializer.Serialize(billNoList));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}