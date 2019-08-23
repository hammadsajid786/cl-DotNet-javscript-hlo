using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BLRMIS.Console.ApiDownloadData.Model;

namespace BLRMIS.Console.ApiDownloadData.DAL
{
    public class DataProvider : SQLProvider
    {

        public DataProvider()
        {
            ConnectioName = "ConnectionString1";
        }

        public DataSet GetAllPendingPayments()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            var ds = new DataSet();
            try
            {
                conn = GetConnection(ConfigurationManager.ConnectionStrings["ReadOnlyConnection"].ConnectionString);
                cmd = GetSqlCommand(conn, "usp_PITBPaymentsSynchronize_GetAllPendingPayments", true);
                var da = new SqlDataAdapter { SelectCommand = cmd };

                da.Fill(ds);
                return ds;
            }
            finally
            {
                conn?.Dispose();
                cmd?.Dispose();
            }

        }

        public void UpdatePendingPayments(bool isSynched, Int64 pitbPaymentsId)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            var ds = new DataSet();
            try
            {
                conn = GetConnection();
                cmd = GetSqlCommand(conn, "usp_PITBPaymentsSynchronize_UpdatePendingPayments", true);
                cmd.Parameters.Add(GetInParameter("@PTIBPaymentsId", SqlDbType.BigInt, pitbPaymentsId));
                cmd.Parameters.Add(GetInParameter("@IsSynched", SqlDbType.Bit, isSynched));
                cmd.ExecuteNonQuery();

            }
            finally
            {
                conn?.Dispose();
                cmd?.Dispose();
            }

        }

        public void LogRequest(string methodName, string challanNumber, string data, Int16 recordType, string createdbyIp)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            try
            {
                conn = GetConnection();
                cmd = GetSqlCommand(conn, "usp_ApiLogs_Insert", true);
                cmd.Parameters.Add(GetOutParameter("@ApiLogId", SqlDbType.Int));
                cmd.Parameters.Add(GetInParameter("@ChallanNumber", SqlDbType.NVarChar, challanNumber));
                cmd.Parameters.Add(GetInParameter("@CreatedByIp", SqlDbType.NVarChar, createdbyIp));
                cmd.Parameters.Add(GetInParameter("@CreatedDate", SqlDbType.DateTime, DateTime.Now));
                cmd.Parameters.Add(GetInParameter("@MethodName", SqlDbType.NVarChar, methodName));
                cmd.Parameters.Add(GetInParameter("@RecordType", SqlDbType.TinyInt, recordType));
                cmd.Parameters.Add(GetInParameter("@RequestData", SqlDbType.NVarChar, data));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
        }

        public string InsertMarkzSahulatData(List<DeliveryStats> model)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string ActionResult = string.Empty;
            try
            {
                for (int i = 0; i < model.Count; i++)
                {
                    conn = GetConnection();
                    cmd = GetSqlCommand(conn, "usp_insertApiDownloadedData", true);
                    cmd.Parameters.Add(GetInParameter("@TotalVisited", SqlDbType.Int, model[i].TotalVisited));
                    cmd.Parameters.Add(GetInParameter("@TotalLandTransfered", SqlDbType.Int, model[i].TotalLandTransfered));
                    cmd.Parameters.Add(GetInParameter("@TotalRegistries", SqlDbType.Int, model[i].TotalRegistries));
                    cmd.Parameters.Add(GetInParameter("@TotalAmount", SqlDbType.Int, model[i].TotalAmount));
                    cmd.Parameters.Add(GetInParameter("@IssuanceCount", SqlDbType.Int, model[i].IssuanceCount));
                    cmd.ExecuteNonQuery();
                }
                ActionResult = "Data Saved Successfully";
            }
            catch (Exception ex)
            {

                ActionResult = ex.ToString();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
            return ActionResult;
        }

        public string InsertGrievanceData()
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string ActionResult = string.Empty;
            try
            {
                conn = GetConnection();
                cmd = GetSqlCommand(conn, "usp_insertGrievanceData", true);
                cmd.ExecuteNonQuery();
                ActionResult = "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                ActionResult = ex.ToString();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
            return ActionResult;
        }

        public string InsertDistrictData(List<DistrictDetail> model)
        {
            SqlConnection conn = null;
            SqlCommand cmd = null;
            string ActionResult = string.Empty;
            try
            {
                for (int i = 0; i < model.Count; i++)
                {
                    conn = GetConnection();
                    cmd = GetSqlCommand(conn, "usp_insertDistrictData", true);
                    cmd.Parameters.Add(GetInParameter("@location_id", SqlDbType.Int, model[i].DistrictId));
                    cmd.Parameters.Add(GetInParameter("@location_name", SqlDbType.NVarChar, model[i].DistrictName));
                    cmd.Parameters.Add(GetInParameter("@active", SqlDbType.Bit, model[i].IsActive));
                    cmd.Parameters.Add(GetInParameter("@OperationStatus", SqlDbType.Int, model[i].OperationStatus));
                    cmd.ExecuteNonQuery();
                }
                ActionResult = "Data Saved Successfully";
            }
            catch (Exception ex)
            {
                ActionResult = ex.ToString();
            }
            finally
            {
                cmd?.Dispose();
                conn?.Dispose();
            }
            return ActionResult;
        }

    }
}