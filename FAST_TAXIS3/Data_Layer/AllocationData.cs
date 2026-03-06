using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class AllocationData
    {
        public static DataTable GetAllAllocations()
        {
            string query = @"SELECT a.TaxiID, a.DriverID, 
                            t.PlateNo, t.Model,
                            s.FName + ' ' + s.LName AS DriverName,
                            o.OfficeName
                            FROM TaxiDriverAllocation a
                            INNER JOIN Taxi t ON a.TaxiID = t.TaxiID
                            INNER JOIN Driver d ON a.DriverID = d.StaffID
                            INNER JOIN Staff s ON d.StaffID = s.StaffID
                            LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                            ORDER BY t.PlateNo, s.FName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetAllocationsByTaxi(int taxiId)
        {
            string query = @"SELECT a.TaxiID, a.DriverID, 
                            t.PlateNo, 
                            s.FName + ' ' + s.LName AS DriverName,
                            s.Phone
                            FROM TaxiDriverAllocation a
                            INNER JOIN Taxi t ON a.TaxiID = t.TaxiID
                            INNER JOIN Driver d ON a.DriverID = d.StaffID
                            INNER JOIN Staff s ON d.StaffID = s.StaffID
                            WHERE a.TaxiID = @TaxiID
                            ORDER BY s.FName";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetAllocationsByDriver(int driverId)
        {
            string query = @"SELECT a.TaxiID, a.DriverID, 
                            t.PlateNo, t.Model,
                            s.FName + ' ' + s.LName AS DriverName
                            FROM TaxiDriverAllocation a
                            INNER JOIN Taxi t ON a.TaxiID = t.TaxiID
                            INNER JOIN Driver d ON a.DriverID = d.StaffID
                            INNER JOIN Staff s ON d.StaffID = s.StaffID
                            WHERE a.DriverID = @DriverID
                            ORDER BY t.PlateNo";

            SqlParameter[] parameters = {
                new SqlParameter("@DriverID", driverId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static bool AddAllocation(int taxiId, int driverId)
        {
            string query = @"INSERT INTO TaxiDriverAllocation (TaxiID, DriverID) 
                           VALUES (@TaxiID, @DriverID)";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@DriverID", driverId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteAllocation(int taxiId, int driverId)
        {
            string query = "DELETE FROM TaxiDriverAllocation WHERE TaxiID = @TaxiID AND DriverID = @DriverID";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@DriverID", driverId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteAllocationsByTaxi(int taxiId)
        {
            string query = "DELETE FROM TaxiDriverAllocation WHERE TaxiID = @TaxiID";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteAllocationsByDriver(int driverId)
        {
            string query = "DELETE FROM TaxiDriverAllocation WHERE DriverID = @DriverID";

            SqlParameter[] parameters = {
                new SqlParameter("@DriverID", driverId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool IsAllocationExists(int taxiId, int driverId)
        {
            string query = "SELECT COUNT(*) FROM TaxiDriverAllocation WHERE TaxiID = @TaxiID AND DriverID = @DriverID";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@DriverID", driverId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public static DataTable GetAvailableDriversForTaxi(int taxiId)
        {
            string query = @"SELECT s.StaffID, s.FName, s.LName, s.Phone, d.LicenseNo
                           FROM Staff s
                           INNER JOIN Driver d ON s.StaffID = d.StaffID
                           WHERE s.StaffID NOT IN (
                               SELECT DriverID FROM TaxiDriverAllocation WHERE TaxiID = @TaxiID
                           )
                           ORDER BY s.FName, s.LName";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetAvailableTaxisForDriver(int driverId)
        {
            string query = @"SELECT t.TaxiID, t.PlateNo, t.Model, o.OfficeName
                           FROM Taxi t
                           LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                           WHERE t.TaxiID NOT IN (
                               SELECT TaxiID FROM TaxiDriverAllocation WHERE DriverID = @DriverID
                           )
                           ORDER BY t.PlateNo";

            SqlParameter[] parameters = {
                new SqlParameter("@DriverID", driverId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int GetDriverCountPerTaxi(int taxiId)
        {
            string query = "SELECT COUNT(*) FROM TaxiDriverAllocation WHERE TaxiID = @TaxiID";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static int GetTaxiCountPerDriver(int driverId)
        {
            string query = "SELECT COUNT(*) FROM TaxiDriverAllocation WHERE DriverID = @DriverID";

            SqlParameter[] parameters = {
                new SqlParameter("@DriverID", driverId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}