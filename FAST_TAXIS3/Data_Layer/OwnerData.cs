using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class OwnerData
    {
        public static DataTable GetAllOwners()
        {
            string query = @"SELECT o.*, 
                            (SELECT COUNT(*) FROM Taxi t WHERE t.OwnerID = o.OwnerID) AS TaxiCount
                            FROM Owner o
                            ORDER BY o.FName, o.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetOwnerById(int ownerId)
        {
            string query = "SELECT * FROM Owner WHERE OwnerID = @OwnerID";
            SqlParameter[] parameters = {
                new SqlParameter("@OwnerID", ownerId)
            };
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddOwner(string fName, string lName, string phone, string address)
        {
            string query = @"INSERT INTO Owner (FName, LName, Phone, Address) 
                           VALUES (@FName, @LName, @Phone, @Address);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", address ?? (object)DBNull.Value)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool UpdateOwner(int ownerId, string fName, string lName, string phone, string address)
        {
            string query = @"UPDATE Owner 
                           SET FName = @FName, LName = @LName, Phone = @Phone, Address = @Address
                           WHERE OwnerID = @OwnerID";

            SqlParameter[] parameters = {
                new SqlParameter("@OwnerID", ownerId),
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", address ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteOwner(int ownerId)
        {
            string query = "DELETE FROM Owner WHERE OwnerID = @OwnerID";
            SqlParameter[] parameters = {
                new SqlParameter("@OwnerID", ownerId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetOwnersWithMultipleTaxis()
        {
            string query = @"SELECT o.OwnerID, o.FName, o.LName, o.Phone, COUNT(t.TaxiID) AS TaxiCount
                           FROM Owner o
                           INNER JOIN Taxi t ON o.OwnerID = t.OwnerID
                           GROUP BY o.OwnerID, o.FName, o.LName, o.Phone
                           HAVING COUNT(t.TaxiID) > 1
                           ORDER BY TaxiCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static bool IsOwnerExists(string fName, string lName)
        {
            string query = "SELECT COUNT(*) FROM Owner WHERE FName = @FName AND LName = @LName";
            SqlParameter[] parameters = {
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public static DataTable GetOwnersByTaxiCount(int minTaxis)
        {
            string query = @"SELECT o.OwnerID, o.FName, o.LName, o.Phone, COUNT(t.TaxiID) AS TaxiCount
                           FROM Owner o
                           LEFT JOIN Taxi t ON o.OwnerID = t.OwnerID
                           GROUP BY o.OwnerID, o.FName, o.LName, o.Phone
                           HAVING COUNT(t.TaxiID) >= @MinTaxis
                           ORDER BY TaxiCount DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@MinTaxis", minTaxis)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
    }
}