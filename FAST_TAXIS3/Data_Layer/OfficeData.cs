using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class OfficeData
    {
        public static DataTable GetAllOffices()
        {
            string query = "SELECT * FROM Office ORDER BY OfficeName";
            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetOfficeById(int officeId)
        {
            string query = "SELECT * FROM Office WHERE OfficeID = @OfficeID";
            SqlParameter[] parameters = {
                new SqlParameter("@OfficeID", officeId)
            };
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddOffice(string officeName, string city, string address, string phone)
        {
            string query = @"INSERT INTO Office (OfficeName, City, Address, Phone) 
                           VALUES (@OfficeName, @City, @Address, @Phone);
                           SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeName", officeName),
                new SqlParameter("@City", city),
                new SqlParameter("@Address", address ?? (object)DBNull.Value),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool UpdateOffice(int officeId, string officeName, string city, string address, string phone)
        {
            string query = @"UPDATE Office 
                           SET OfficeName = @OfficeName, 
                               City = @City, 
                               Address = @Address, 
                               Phone = @Phone 
                           WHERE OfficeID = @OfficeID";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeID", officeId),
                new SqlParameter("@OfficeName", officeName),
                new SqlParameter("@City", city),
                new SqlParameter("@Address", address ?? (object)DBNull.Value),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteOffice(int officeId)
        {
            string query = "DELETE FROM Office WHERE OfficeID = @OfficeID";
            SqlParameter[] parameters = {
                new SqlParameter("@OfficeID", officeId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetManagersByOffice()
        {
            string query = @"
                SELECT o.OfficeName, o.City, s.FName + ' ' + s.LName AS ManagerName, s.Phone
                FROM Office o
                INNER JOIN Staff s ON o.OfficeID = s.OfficeID
                INNER JOIN Manager m ON s.StaffID = m.StaffID
                ORDER BY o.OfficeName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetOfficesWithStaffCount()
        {
            string query = @"
                SELECT o.OfficeID, o.OfficeName, o.City, COUNT(s.StaffID) AS StaffCount
                FROM Office o
                LEFT JOIN Staff s ON o.OfficeID = s.OfficeID
                GROUP BY o.OfficeID, o.OfficeName, o.City
                ORDER BY o.OfficeName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetOfficesByCity(string city)
        {
            string query = "SELECT * FROM Office WHERE City = @City ORDER BY OfficeName";
            SqlParameter[] parameters = {
                new SqlParameter("@City", city)
            };
            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static bool IsOfficeExists(string officeName)
        {
            string query = "SELECT COUNT(*) FROM Office WHERE OfficeName = @OfficeName";
            SqlParameter[] parameters = {
                new SqlParameter("@OfficeName", officeName)
            };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null && Convert.ToInt32(result) > 0;
        }
    }
}