using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class TaxiData
    {
        public static DataTable GetAllTaxis()
        {
            string query = @"SELECT t.*, o.OfficeName, ow.FName + ' ' + ow.LName AS OwnerName,
                            (SELECT COUNT(*) FROM TaxiDriverAllocation a WHERE a.TaxiID = t.TaxiID) AS DriverCount
                            FROM Taxi t
                            LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                            LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
                            ORDER BY t.PlateNo";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetTaxisByOffice(int officeId)
        {
            string query = @"SELECT t.*, o.OfficeName, ow.FName + ' ' + ow.LName AS OwnerName,
                            (SELECT COUNT(*) FROM TaxiDriverAllocation a WHERE a.TaxiID = t.TaxiID) AS DriverCount
                            FROM Taxi t
                            LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                            LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
                            WHERE t.OfficeID = @OfficeID
                            ORDER BY t.PlateNo";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeID", officeId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetTaxisByOffice(string officeName)
        {
            string query = @"SELECT t.*, o.OfficeName, ow.FName + ' ' + ow.LName AS OwnerName,
                            (SELECT COUNT(*) FROM TaxiDriverAllocation a WHERE a.TaxiID = t.TaxiID) AS DriverCount
                            FROM Taxi t
                            LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                            LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
                            WHERE o.OfficeName = @OfficeName
                            ORDER BY t.PlateNo";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeName", officeName)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddTaxi(string plateNo, string manufacturer, string model, int year, DateTime? nextServiceDate, int officeId, int ownerId)
        {
            string query = @"INSERT INTO Taxi (PlateNo, Manufacturer, Model, Year, NextServiceDate, OfficeID, OwnerID) 
                           VALUES (@PlateNo, @Manufacturer, @Model, @Year, @NextServiceDate, @OfficeID, @OwnerID);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@PlateNo", plateNo),
                new SqlParameter("@Manufacturer", manufacturer ?? (object)DBNull.Value),
                new SqlParameter("@Model", model ?? (object)DBNull.Value),
                new SqlParameter("@Year", year),
                new SqlParameter("@NextServiceDate", nextServiceDate ?? (object)DBNull.Value),
                new SqlParameter("@OfficeID", officeId),
                new SqlParameter("@OwnerID", ownerId)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool UpdateTaxi(int taxiId, string plateNo, string manufacturer, string model, int year, DateTime? nextServiceDate, int officeId, int ownerId)
        {
            string query = @"UPDATE Taxi 
                           SET PlateNo = @PlateNo, Manufacturer = @Manufacturer, Model = @Model, 
                               Year = @Year, NextServiceDate = @NextServiceDate, 
                               OfficeID = @OfficeID, OwnerID = @OwnerID
                           WHERE TaxiID = @TaxiID";

            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId),
                new SqlParameter("@PlateNo", plateNo),
                new SqlParameter("@Manufacturer", manufacturer ?? (object)DBNull.Value),
                new SqlParameter("@Model", model ?? (object)DBNull.Value),
                new SqlParameter("@Year", year),
                new SqlParameter("@NextServiceDate", nextServiceDate ?? (object)DBNull.Value),
                new SqlParameter("@OfficeID", officeId),
                new SqlParameter("@OwnerID", ownerId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteTaxi(int taxiId)
        {
            string query = "DELETE FROM Taxi WHERE TaxiID = @TaxiID";
            SqlParameter[] parameters = {
                new SqlParameter("@TaxiID", taxiId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static int GetTotalTaxis()
        {
            string query = "SELECT COUNT(*) FROM Taxi";
            object result = DatabaseHelper.ExecuteScalar(query);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static DataTable GetDriverCountPerTaxi()
        {
            string query = @"SELECT t.TaxiID, t.PlateNo, t.Model, 
                            COUNT(a.DriverID) AS DriverCount
                            FROM Taxi t
                            LEFT JOIN TaxiDriverAllocation a ON t.TaxiID = a.TaxiID
                            GROUP BY t.TaxiID, t.PlateNo, t.Model
                            ORDER BY DriverCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetTaxisDueForMaintenance(DateTime serviceDate)
        {
            string query = @"SELECT t.*, o.OfficeName, ow.FName + ' ' + ow.LName AS OwnerName
                           FROM Taxi t
                           LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                           LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
                           WHERE t.NextServiceDate <= @ServiceDate
                           ORDER BY t.NextServiceDate";

            SqlParameter[] parameters = {
                new SqlParameter("@ServiceDate", serviceDate)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetTaxisByOwner(int ownerId)
        {
            string query = @"SELECT t.*, o.OfficeName
                           FROM Taxi t
                           LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                           WHERE t.OwnerID = @OwnerID
                           ORDER BY t.PlateNo";

            SqlParameter[] parameters = {
                new SqlParameter("@OwnerID", ownerId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static bool IsPlateNoExists(string plateNo)
        {
            string query = "SELECT COUNT(*) FROM Taxi WHERE PlateNo = @PlateNo";
            SqlParameter[] parameters = {
                new SqlParameter("@PlateNo", plateNo)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}