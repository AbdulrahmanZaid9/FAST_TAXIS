using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class JobData
    {
        public static DataTable GetAllJobs()
        {
            string query = @"
        SELECT j.*, 
               c.FName + ' ' + c.LName AS ClientName,
               s.FName + ' ' + s.LName AS DriverName,
               t.PlateNo,
               b.CompanyName,  -- ✅ هنا التصحيح: نجيب CompanyName من BusinessClient
               CASE 
                   WHEN j.ContractID IS NOT NULL THEN 'Contract'
                   ELSE 'Ad-hoc'
               END AS JobType
        FROM Job j
        INNER JOIN Client c ON j.ClientID = c.ClientID
        INNER JOIN Driver d ON j.DriverID = d.StaffID
        INNER JOIN Staff s ON d.StaffID = s.StaffID
        INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
        LEFT JOIN Contract ct ON j.ContractID = ct.ContractID
        LEFT JOIN BusinessClient b ON c.ClientID = b.ClientID  -- ✅ ربط إضافي عشان نجيب CompanyName
        ORDER BY j.PickupDateTime DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetJobById(int jobId)
        {
            string query = @"SELECT j.*, 
                            c.FName + ' ' + c.LName AS ClientName, c.Phone, c.Address, c.City,
                            s.FName + ' ' + s.LName AS DriverName, s.Phone AS DriverPhone,
                            t.PlateNo, t.Model,
                            b.CompanyName, ct.FixedFee  -- ✅ b.CompanyName
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           LEFT JOIN Contract ct ON j.ContractID = ct.ContractID
                           LEFT JOIN BusinessClient b ON c.ClientID = b.ClientID  -- ✅ ربط BusinessClient
                           WHERE j.JobID = @JobID";

            SqlParameter[] parameters = {
        new SqlParameter("@JobID", jobId)
    };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddJob(DateTime pickupDateTime, string pickupAddress, string dropoffAddress,
                                string status, decimal? mileageKm, decimal? chargeAmount, string failReason,
                                int clientId, int driverId, int taxiId, int? contractId)
        {
            string query = @"INSERT INTO Job (PickupDateTime, PickupAddress, DropoffAddress, Status, 
                                            MileageKm, ChargeAmount, FailReason, ClientID, DriverID, TaxiID, ContractID) 
                           VALUES (@PickupDateTime, @PickupAddress, @DropoffAddress, @Status, 
                                   @MileageKm, @ChargeAmount, @FailReason, @ClientID, @DriverID, @TaxiID, @ContractID);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@PickupDateTime", pickupDateTime),
                new SqlParameter("@PickupAddress", pickupAddress),
                new SqlParameter("@DropoffAddress", dropoffAddress),
                new SqlParameter("@Status", status),
                new SqlParameter("@MileageKm", mileageKm ?? (object)DBNull.Value),
                new SqlParameter("@ChargeAmount", chargeAmount ?? (object)DBNull.Value),
                new SqlParameter("@FailReason", failReason ?? (object)DBNull.Value),
                new SqlParameter("@ClientID", clientId),
                new SqlParameter("@DriverID", driverId),
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@ContractID", contractId ?? (object)DBNull.Value)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool UpdateJob(int jobId, DateTime pickupDateTime, string pickupAddress, string dropoffAddress,
                                    string status, decimal? mileageKm, decimal? chargeAmount, string failReason,
                                    int clientId, int driverId, int taxiId, int? contractId)
        {
            string query = @"UPDATE Job 
                           SET PickupDateTime = @PickupDateTime, 
                               PickupAddress = @PickupAddress, 
                               DropoffAddress = @DropoffAddress, 
                               Status = @Status, 
                               MileageKm = @MileageKm, 
                               ChargeAmount = @ChargeAmount, 
                               FailReason = @FailReason, 
                               ClientID = @ClientID, 
                               DriverID = @DriverID, 
                               TaxiID = @TaxiID, 
                               ContractID = @ContractID
                           WHERE JobID = @JobID";

            SqlParameter[] parameters = {
                new SqlParameter("@JobID", jobId),
                new SqlParameter("@PickupDateTime", pickupDateTime),
                new SqlParameter("@PickupAddress", pickupAddress),
                new SqlParameter("@DropoffAddress", dropoffAddress),
                new SqlParameter("@Status", status),
                new SqlParameter("@MileageKm", mileageKm ?? (object)DBNull.Value),
                new SqlParameter("@ChargeAmount", chargeAmount ?? (object)DBNull.Value),
                new SqlParameter("@FailReason", failReason ?? (object)DBNull.Value),
                new SqlParameter("@ClientID", clientId),
                new SqlParameter("@DriverID", driverId),
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@ContractID", contractId ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteJob(int jobId)
        {
            string query = "DELETE FROM Job WHERE JobID = @JobID";
            SqlParameter[] parameters = {
                new SqlParameter("@JobID", jobId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetJobsByDriver(int driverId, DateTime? date = null)
        {
            string query = @"SELECT j.*, c.FName + ' ' + c.LName AS ClientName, t.PlateNo
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           WHERE j.DriverID = @DriverID";

            if (date.HasValue)
            {
                query += " AND CAST(j.PickupDateTime AS DATE) = @JobDate";
            }

            query += " ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = date.HasValue
                ? new SqlParameter[] {
                    new SqlParameter("@DriverID", driverId),
                    new SqlParameter("@JobDate", date.Value.Date)
                  }
                : new SqlParameter[] {
                    new SqlParameter("@DriverID", driverId)
                  };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
        public static DataTable GetJobsByOffice(int officeId)
        {
            string query = @"
        SELECT j.*, 
               c.FName + ' ' + c.LName AS ClientName,
               s.FName + ' ' + s.LName AS DriverName,
               t.PlateNo
        FROM Job j
        INNER JOIN Client c ON j.ClientID = c.ClientID
        INNER JOIN Driver d ON j.DriverID = d.StaffID
        INNER JOIN Staff s ON d.StaffID = s.StaffID
        INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
        WHERE t.OfficeID = @OfficeID
        ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = {
        new SqlParameter("@OfficeID", officeId)
    };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
        public static DataTable GetJobsByTaxi(int taxiId)
        {
            string query = @"SELECT j.*, c.FName + ' ' + c.LName AS ClientName, 
                            s.FName + ' ' + s.LName AS DriverName
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           WHERE j.TaxiID = @TaxiID
                           ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetJobsByClient(int clientId)
        {
            string query = @"SELECT j.*, s.FName + ' ' + s.LName AS DriverName, t.PlateNo
                           FROM Job j
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           WHERE j.ClientID = @ClientID
                           ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetJobsByContract(int contractId)
        {
            string query = @"SELECT j.*, c.FName + ' ' + c.LName AS ClientName,
                            s.FName + ' ' + s.LName AS DriverName, t.PlateNo
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           WHERE j.ContractID = @ContractID
                           ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetJobsByDateRange(DateTime startDate, DateTime endDate)
        {
            string query = @"SELECT j.*, c.FName + ' ' + c.LName AS ClientName,
                            s.FName + ' ' + s.LName AS DriverName, t.PlateNo
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           WHERE CAST(j.PickupDateTime AS DATE) BETWEEN @StartDate AND @EndDate
                           ORDER BY j.PickupDateTime DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@StartDate", startDate.Date),
                new SqlParameter("@EndDate", endDate.Date)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int GetJobCountByTaxi(int taxiId)
        {
            string query = "SELECT COUNT(*) FROM Job WHERE TaxiID = @TaxiID";
            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static int GetJobCountByDriver(int driverId)
        {
            string query = "SELECT COUNT(*) FROM Job WHERE DriverID = @DriverID";
            SqlParameter[] parameters = {
                new SqlParameter("@DriverID", driverId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static DataTable GetJobCountPerTaxi()
        {
            string query = @"SELECT t.TaxiID, t.PlateNo, t.Model, COUNT(j.JobID) AS JobCount
                           FROM Taxi t
                           LEFT JOIN Job j ON t.TaxiID = j.TaxiID
                           GROUP BY t.TaxiID, t.PlateNo, t.Model
                           ORDER BY JobCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetJobCountPerDriver()
        {
            string query = @"SELECT s.StaffID, s.FName + ' ' + s.LName AS DriverName, 
                            COUNT(j.JobID) AS JobCount
                           FROM Driver d
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           LEFT JOIN Job j ON d.StaffID = j.DriverID
                           GROUP BY s.StaffID, s.FName, s.LName
                           ORDER BY JobCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static decimal GetAverageChargeForPrivateClients()
        {
            string query = @"SELECT AVG(j.ChargeAmount) 
                           FROM Job j
                           INNER JOIN PrivateClient p ON j.ClientID = p.ClientID
                           WHERE j.ChargeAmount IS NOT NULL AND j.Status = 'Completed'";

            object result = DatabaseHelper.ExecuteScalar(query);
            return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public static DataTable GetTotalChargedPerCar(DateTime monthDate)
        {
            string query = @"SELECT t.TaxiID, t.PlateNo, t.Model, 
                            SUM(j.ChargeAmount) AS TotalCharged,
                            COUNT(j.JobID) AS TotalJobs
                           FROM Taxi t
                           INNER JOIN Job j ON t.TaxiID = j.TaxiID
                           WHERE MONTH(j.PickupDateTime) = @Month AND YEAR(j.PickupDateTime) = @Year
                           AND j.Status = 'Completed'
                           GROUP BY t.TaxiID, t.PlateNo, t.Model
                           ORDER BY TotalCharged DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@Month", monthDate.Month),
                new SqlParameter("@Year", monthDate.Year)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetFailedJobs()
        {
            string query = @"SELECT j.*, c.FName + ' ' + c.LName AS ClientName,
                            s.FName + ' ' + s.LName AS DriverName, t.PlateNo
                           FROM Job j
                           INNER JOIN Client c ON j.ClientID = c.ClientID
                           INNER JOIN Driver d ON j.DriverID = d.StaffID
                           INNER JOIN Staff s ON d.StaffID = s.StaffID
                           INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
                           WHERE j.Status = 'Failed'
                           ORDER BY j.PickupDateTime DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static bool CompleteJob(int jobId, decimal mileageKm, decimal chargeAmount)
        {
            string query = @"UPDATE Job 
                           SET Status = 'Completed', MileageKm = @MileageKm, ChargeAmount = @ChargeAmount
                           WHERE JobID = @JobID";

            SqlParameter[] parameters = {
                new SqlParameter("@JobID", jobId),
                new SqlParameter("@MileageKm", mileageKm),
                new SqlParameter("@ChargeAmount", chargeAmount)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool FailJob(int jobId, string failReason)
        {
            string query = @"UPDATE Job 
                           SET Status = 'Failed', FailReason = @FailReason
                           WHERE JobID = @JobID";

            SqlParameter[] parameters = {
                new SqlParameter("@JobID", jobId),
                new SqlParameter("@FailReason", failReason)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }
        public static DataTable GetAverageFeeForPrivateClients()
        {
            return ReportData.GetAverageFeePrivateClients();
        }
    }
}