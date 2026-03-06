using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class ContractData
    {
        public static DataTable GetAllContracts()
        {
            string query = @"SELECT co.*, c.FName + ' ' + c.LName AS ClientName, c.Phone, b.CompanyName
                           FROM Contract co
                           INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                           INNER JOIN Client c ON b.ClientID = c.ClientID
                           ORDER BY co.StartDate DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetContractById(int contractId)
        {
            string query = @"SELECT co.*, c.FName + ' ' + c.LName AS ClientName, c.Phone, b.CompanyName, c.Address, c.City
                           FROM Contract co
                           INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                           INNER JOIN Client c ON b.ClientID = c.ClientID
                           WHERE co.ContractID = @ContractID";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetContractsByClient(int clientId)
        {
            string query = @"
        SELECT co.ContractID, co.StartDate, co.EndDate, co.AgreedNumJobs, co.FixedFee,
               c.FName + ' ' + c.LName AS ClientName,
               b.CompanyName
        FROM Contract co
        INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
        INNER JOIN Client c ON b.ClientID = c.ClientID
        WHERE co.ClientID = @ClientID
        ORDER BY co.StartDate DESC";

            SqlParameter[] parameters = {
        new SqlParameter("@ClientID", clientId)
    };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddContract(DateTime startDate, DateTime? endDate, int agreedNumJobs, decimal fixedFee, int clientId)
        {
            string query = @"INSERT INTO Contract (StartDate, EndDate, AgreedNumJobs, FixedFee, ClientID) 
                           VALUES (@StartDate, @EndDate, @AgreedNumJobs, @FixedFee, @ClientID);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate ?? (object)DBNull.Value),
                new SqlParameter("@AgreedNumJobs", agreedNumJobs),
                new SqlParameter("@FixedFee", fixedFee),
                new SqlParameter("@ClientID", clientId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool UpdateContract(int contractId, DateTime startDate, DateTime? endDate, int agreedNumJobs, decimal fixedFee, int clientId)
        {
            string query = @"UPDATE Contract 
                           SET StartDate = @StartDate, EndDate = @EndDate, 
                               AgreedNumJobs = @AgreedNumJobs, FixedFee = @FixedFee, ClientID = @ClientID
                           WHERE ContractID = @ContractID";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate ?? (object)DBNull.Value),
                new SqlParameter("@AgreedNumJobs", agreedNumJobs),
                new SqlParameter("@FixedFee", fixedFee),
                new SqlParameter("@ClientID", clientId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteContract(int contractId)
        {
            string query = "DELETE FROM Contract WHERE ContractID = @ContractID";
            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetCurrentContracts()
        {
            string query = @"
        SELECT 
            co.ContractID,
            co.StartDate,
            co.EndDate,
            co.AgreedNumJobs,
            co.FixedFee,
            co.ClientID,
            c.FName + ' ' + c.LName AS ClientName,
            ISNULL(b.CompanyName, '') AS CompanyName
        FROM Contract co
        INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
        INNER JOIN Client c ON b.ClientID = c.ClientID
        WHERE co.EndDate IS NULL OR co.EndDate >= GETDATE()
        ORDER BY co.StartDate DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetContractsByCity(string city)
        {
            string query = @"SELECT co.*, c.FName + ' ' + c.LName AS ClientName, b.CompanyName, c.Address, c.City
                           FROM Contract co
                           INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                           INNER JOIN Client c ON b.ClientID = c.ClientID
                           WHERE c.City = @City AND (co.EndDate IS NULL OR co.EndDate >= GETDATE())
                           ORDER BY co.StartDate DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@City", city)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetContractJobSummary(int contractId)
        {
            string query = @"SELECT 
                            co.ContractID, co.StartDate, co.EndDate, co.AgreedNumJobs, co.FixedFee,
                            COUNT(j.JobID) AS TotalJobsDone,
                            ISNULL(SUM(j.MileageKm), 0) AS TotalKm,
                            ISNULL(SUM(j.ChargeAmount), 0) AS TotalCharged,
                            CASE 
                                WHEN co.AgreedNumJobs > 0 
                                THEN (COUNT(j.JobID) * 100.0 / co.AgreedNumJobs)
                                ELSE 0 
                            END AS CompletionPercentage
                           FROM Contract co
                           LEFT JOIN Job j ON co.ContractID = j.ContractID
                           WHERE co.ContractID = @ContractID
                           GROUP BY co.ContractID, co.StartDate, co.EndDate, co.AgreedNumJobs, co.FixedFee";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetExpiringContracts(int daysThreshold)
        {
            string query = @"SELECT co.*, c.FName + ' ' + c.LName AS ClientName, b.CompanyName, c.Phone
                           FROM Contract co
                           INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                           INNER JOIN Client c ON b.ClientID = c.ClientID
                           WHERE co.EndDate IS NOT NULL 
                           AND co.EndDate BETWEEN GETDATE() AND DATEADD(DAY, @Days, GETDATE())
                           ORDER BY co.EndDate";

            SqlParameter[] parameters = {
                new SqlParameter("@Days", daysThreshold)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static bool IsClientHasActiveContract(int clientId)
        {
            string query = @"SELECT COUNT(*) FROM Contract 
                           WHERE ClientID = @ClientID 
                           AND (EndDate IS NULL OR EndDate >= GETDATE())";

            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public static int GetRemainingJobs(int contractId)
        {
            string query = @"SELECT 
                            co.AgreedNumJobs - COUNT(j.JobID) AS RemainingJobs
                           FROM Contract co
                           LEFT JOIN Job j ON co.ContractID = j.ContractID
                           WHERE co.ContractID = @ContractID
                           GROUP BY co.AgreedNumJobs";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["RemainingJobs"] != DBNull.Value)
                return Convert.ToInt32(dt.Rows[0]["RemainingJobs"]);

            return 0;
        }
    }
}