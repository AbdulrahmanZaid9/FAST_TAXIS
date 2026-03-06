using System;
using System.Data;
using System.Data.SqlClient;
using FAST_TAXIS3.Helpers;

namespace FAST_TAXIS3.Data
{
    public static class StaffData
    {
        public static DataTable GetAllStaff()
        {
            string query = @"SELECT s.*, o.OfficeName,
                            CASE 
                                WHEN m.StaffID IS NOT NULL THEN 'Manager'
                                WHEN a.StaffID IS NOT NULL THEN 'Admin'
                                WHEN d.StaffID IS NOT NULL THEN 'Driver'
                                ELSE 'Staff'
                            END AS StaffType
                            FROM Staff s
                            LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                            LEFT JOIN Manager m ON s.StaffID = m.StaffID
                            LEFT JOIN AdminStaff a ON s.StaffID = a.StaffID
                            LEFT JOIN Driver d ON s.StaffID = d.StaffID
                            ORDER BY s.FName, s.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetStaffByOffice(int officeId)
        {
            string query = @"SELECT s.*, o.OfficeName,
                            CASE 
                                WHEN m.StaffID IS NOT NULL THEN 'Manager'
                                WHEN a.StaffID IS NOT NULL THEN 'Admin'
                                WHEN d.StaffID IS NOT NULL THEN 'Driver'
                                ELSE 'Staff'
                            END AS StaffType
                            FROM Staff s
                            LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                            LEFT JOIN Manager m ON s.StaffID = m.StaffID
                            LEFT JOIN AdminStaff a ON s.StaffID = a.StaffID
                            LEFT JOIN Driver d ON s.StaffID = d.StaffID
                            WHERE s.OfficeID = @OfficeID
                            ORDER BY s.FName, s.LName";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeID", officeId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static int AddStaff(string fName, string lName, string phone, string gender, DateTime dob, string address, int officeId, string password)
        {
            string query = @"INSERT INTO Staff (FName, LName, Phone, Gender, DateOfBirth, Address, OfficeID, Password) 
                   VALUES (@FName, @LName, @Phone, @Gender, @DOB, @Address, @OfficeID, @Password);
                   SELECT SCOPE_IDENTITY();";

            SqlParameter[] parameters = {
        new SqlParameter("@FName", fName),
        new SqlParameter("@LName", lName),
        new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
        new SqlParameter("@Gender", gender ?? (object)DBNull.Value),
        new SqlParameter("@DOB", dob),
        new SqlParameter("@Address", address ?? (object)DBNull.Value),
        new SqlParameter("@OfficeID", officeId),
        new SqlParameter("@Password", password)  
    };

            object result = DatabaseHelper.ExecuteScalar(query, parameters);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        public static bool AddManager(int staffId)
        {
            string query = "INSERT INTO Manager (StaffID) VALUES (@StaffID)";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool AddAdminStaff(int staffId, string jobTitle)
        {
            string query = "INSERT INTO AdminStaff (StaffID, JobTitle) VALUES (@StaffID, @JobTitle)";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId),
                new SqlParameter("@JobTitle", jobTitle)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool AddDriver(int staffId, string licenseNo)
        {
            string query = "INSERT INTO Driver (StaffID, LicenseNo) VALUES (@StaffID, @LicenseNo)";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId),
                new SqlParameter("@LicenseNo", licenseNo)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool UpdateStaff(int staffId, string fName, string lName, string phone, string gender, DateTime dob, string address, int officeId)
        {
            string query = @"UPDATE Staff 
                           SET FName = @FName, LName = @LName, Phone = @Phone, 
                               Gender = @Gender, DateOfBirth = @DOB, Address = @Address, OfficeID = @OfficeID
                           WHERE StaffID = @StaffID";

            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId),
                new SqlParameter("@FName", fName),
                new SqlParameter("@LName", lName),
                new SqlParameter("@Phone", phone ?? (object)DBNull.Value),
                new SqlParameter("@Gender", gender ?? (object)DBNull.Value),
                new SqlParameter("@DOB", dob),
                new SqlParameter("@Address", address ?? (object)DBNull.Value),
                new SqlParameter("@OfficeID", officeId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool UpdateDriver(int staffId, string licenseNo)
        {
            string query = "UPDATE Driver SET LicenseNo = @LicenseNo WHERE StaffID = @StaffID";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId),
                new SqlParameter("@LicenseNo", licenseNo)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool UpdateAdminStaff(int staffId, string jobTitle)
        {
            string query = "UPDATE AdminStaff SET JobTitle = @JobTitle WHERE StaffID = @StaffID";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId),
                new SqlParameter("@JobTitle", jobTitle)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static bool DeleteStaff(int staffId)
        {
            string query = "DELETE FROM Staff WHERE StaffID = @StaffID";
            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId)
            };

            int rowsAffected = DatabaseHelper.ExecuteNonQuery(query, parameters);
            return rowsAffected > 0;
        }

        public static DataTable GetAllDrivers()
        {
            string query = @"SELECT s.*, d.LicenseNo, o.OfficeName 
                           FROM Staff s
                           INNER JOIN Driver d ON s.StaffID = d.StaffID
                           LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                           ORDER BY s.FName, s.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetFemaleDriversByOffice(string officeName)
        {
            string query = @"SELECT s.FName, s.LName, s.Phone, o.OfficeName
                           FROM Staff s
                           INNER JOIN Driver d ON s.StaffID = d.StaffID
                           LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                           WHERE s.Gender = 'F' AND o.OfficeName = @OfficeName
                           ORDER BY s.FName, s.LName";

            SqlParameter[] parameters = {
                new SqlParameter("@OfficeName", officeName)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        public static DataTable GetDriversOver25()
        {
            string query = @"SELECT s.FName, s.LName, s.DateOfBirth, s.Phone, o.OfficeName
                           FROM Staff s
                           INNER JOIN Driver d ON s.StaffID = d.StaffID
                           LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                           WHERE DATEDIFF(YEAR, s.DateOfBirth, GETDATE()) > 25
                           ORDER BY s.FName, s.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        public static DataTable GetStaffType(int staffId)
        {
            string query = @"SELECT 
                            CASE 
                                WHEN m.StaffID IS NOT NULL THEN 'Manager'
                                WHEN a.StaffID IS NOT NULL THEN 'Admin'
                                WHEN d.StaffID IS NOT NULL THEN 'Driver'
                                ELSE 'Staff'
                            END AS StaffType
                            FROM Staff s
                            LEFT JOIN Manager m ON s.StaffID = m.StaffID
                            LEFT JOIN AdminStaff a ON s.StaffID = a.StaffID
                            LEFT JOIN Driver d ON s.StaffID = d.StaffID
                            WHERE s.StaffID = @StaffID";

            SqlParameter[] parameters = {
                new SqlParameter("@StaffID", staffId)
            };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
                return dt;

            return null;
        }
    }
}