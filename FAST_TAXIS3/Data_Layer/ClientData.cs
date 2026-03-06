using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class ClientData
    {
        public static DataTable GetAllClients()
        {
            string query = @"SELECT c.*,
                            CASE 
                                WHEN p.ClientID IS NOT NULL THEN 'Private'
                                WHEN b.ClientID IS NOT NULL THEN 'Business'
                                ELSE 'Unknown'
                            END AS ClientType,
                            b.CompanyName
                            FROM Client c
                            LEFT JOIN PrivateClient p ON c.ClientID = p.ClientID
                            LEFT JOIN BusinessClient b ON c.ClientID = b.ClientID
                            ORDER BY c.FName, c.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetClientById(int clientId)
        {
            string query = @"SELECT c.*,
                            CASE 
                                WHEN p.ClientID IS NOT NULL THEN 'Private'
                                WHEN b.ClientID IS NOT NULL THEN 'Business'
                                ELSE 'Unknown'
                            END AS ClientType,
                            b.CompanyName
                            FROM Client c
                            LEFT JOIN PrivateClient p ON c.ClientID = p.ClientID
                            LEFT JOIN BusinessClient b ON c.ClientID = b.ClientID
                            WHERE c.ClientID = @ClientID";

            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddClient(string fName, string lName, string phone, string address, string city)
        {
            string query = @"INSERT INTO Client (FName, LName, Phone, Address, City) 
                           VALUES (@FName, @LName, @Phone, @Address, @City);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", address ?? (object)DBNull.Value),
                new SqlParameter("@City", city ?? (object)DBNull.Value)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool AddPrivateClient(int clientId)
        {
            string query = "INSERT INTO PrivateClient (ClientID) VALUES (@ClientID)";
            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool AddBusinessClient(int clientId, string companyName)
        {
            string query = "INSERT INTO BusinessClient (ClientID, CompanyName) VALUES (@ClientID, @CompanyName)";
            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId),
                new SqlParameter("@CompanyName", companyName)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool UpdateClient(int clientId, string fName, string lName, string phone, string address, string city)
        {
            string query = @"UPDATE Client 
                           SET FName = @FName, LName = @LName, Phone = @Phone, 
                               Address = @Address, City = @City
                           WHERE ClientID = @ClientID";

            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId),
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@Address", address ?? (object)DBNull.Value),
                new SqlParameter("@City", city ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool UpdateBusinessClient(int clientId, string companyName)
        {
            string query = "UPDATE BusinessClient SET CompanyName = @CompanyName WHERE ClientID = @ClientID";
            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId),
                new SqlParameter("@CompanyName", companyName)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteClient(int clientId)
        {
            string query = "DELETE FROM Client WHERE ClientID = @ClientID";
            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetPrivateClients()
        {
            string query = @"SELECT c.*
                           FROM Client c
                           INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                           ORDER BY c.FName, c.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetBusinessClients()
        {
            string query = @"SELECT c.*, b.CompanyName
                           FROM Client c
                           INNER JOIN BusinessClient b ON c.ClientID = b.ClientID
                           ORDER BY b.CompanyName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetBusinessClientsByCity(string city)
        {
            string query = @"SELECT c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City, b.CompanyName
                           FROM Client c
                           INNER JOIN BusinessClient b ON c.ClientID = b.ClientID
                           WHERE c.City = @City
                           ORDER BY b.CompanyName";

            SqlParameter[] parameters = {
                new SqlParameter("@City", city)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetPrivateClientCountByCity()
        {
            string query = @"SELECT c.City, COUNT(c.ClientID) AS ClientCount
                           FROM Client c
                           INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                           WHERE c.City IS NOT NULL
                           GROUP BY c.City
                           ORDER BY ClientCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetPrivateClientsByHireDate(DateTime hireDate)
        {
            string query = @"SELECT DISTINCT c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City
                           FROM Client c
                           INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                           INNER JOIN Job j ON c.ClientID = j.ClientID
                           WHERE YEAR(j.PickupDateTime) = @Year AND MONTH(j.PickupDateTime) = @Month
                           ORDER BY c.FName, c.LName";

            SqlParameter[] parameters = {
                new SqlParameter("@Year", hireDate.Year),
                new SqlParameter("@Month", hireDate.Month)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetPrivateClientsWithMoreThanXHires(int hireCount)
        {
            string query = @"SELECT c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City, COUNT(j.JobID) AS TotalHires
                           FROM Client c
                           INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                           INNER JOIN Job j ON c.ClientID = j.ClientID
                           GROUP BY c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City
                           HAVING COUNT(j.JobID) > @HireCount
                           ORDER BY TotalHires DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@HireCount", hireCount)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static bool IsClientExists(string fName, string lName, string phone)
        {
            string query = "SELECT COUNT(*) FROM Client WHERE FName = @FName AND LName = @LName AND Phone = @Phone";
            SqlParameter[] parameters = {
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }

        public static string GetClientType(int clientId)
        {
            string query = @"SELECT 
                            CASE 
                                WHEN p.ClientID IS NOT NULL THEN 'Private'
                                WHEN b.ClientID IS NOT NULL THEN 'Business'
                                ELSE 'Unknown'
                            END AS ClientType
                            FROM Client c
                            LEFT JOIN PrivateClient p ON c.ClientID = p.ClientID
                            LEFT JOIN BusinessClient b ON c.ClientID = b.ClientID
                            WHERE c.ClientID = @ClientID";

            SqlParameter[] parameters = {
                new SqlParameter("@ClientID", clientId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
                return dt.Rows[0]["ClientType"].ToString();

            return "Unknown";
        }
    }
}