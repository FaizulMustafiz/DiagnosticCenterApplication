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
    public class UnpaidBillReportGateway
    {
        string connectionString =
            WebConfigurationManager.ConnectionStrings["DiagnosticCenterConnectionString"].ConnectionString;


        internal List<UnpaidBillReport> GetUnPaidBillList(string endDate , string startDate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            string query = @"SELECT * FROM Patient WHERE DueDate BETWEEN '" + endDate + "' AND '" + startDate + "'";

            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<UnpaidBillReport> unPaidBillList = new List<UnpaidBillReport>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UnpaidBillReport unpaidBill = new UnpaidBillReport();

                    unpaidBill.BillNo = reader["Billno"].ToString();
                    unpaidBill.MobileNo = reader["MobileNo"].ToString();
                    unpaidBill.PatientName = reader["PatientName"].ToString();
                    unpaidBill.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());
                    unpaidBill.TotalAmount = Convert.ToDecimal(reader["TotalAmount"].ToString());

                    unPaidBillList.Add(unpaidBill);
                }
                reader.Close();
            }
            connection.Close();
            return unPaidBillList;

        }


        //internal List<DueAmountView> GetDueAmmount()
        //{
        //    SqlConnection connection = new SqlConnection(connectionString);

        //    string query = "SELECT * FROM DueAmmount";

        //    SqlCommand command = new SqlCommand(query, connection);
        //    connection.Open();

        //    SqlDataReader reader = command.ExecuteReader();

        //    List<DueAmountView> dueAmountList = new List<DueAmountView>();
        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            DueAmountView dueAmount = new DueAmountView();
        //            dueAmount.TotalAmount = Convert.ToDecimal(reader["TotalAmmount"].ToString());
        //            dueAmount.PaymentStatus = Convert.ToDecimal(reader["PaymentStatus"].ToString());
        //            dueAmount.DueAmount = Convert.ToDecimal(reader["DueAmount"].ToString());

        //            dueAmountList.Add(dueAmount);
        //        }
        //        reader.Close();
        //    }
        //    connection.Close();
        //    return dueAmountList;

        //}
    }
}