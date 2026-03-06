using FAST_TAXIS3.Helpers;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FAST_TAXIS3.Data
{
    public static class ReportData
    {
        // a) The names and phone numbers of the Managers at each office.
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

        // b) The names of all female drivers based in the Cyberjaya office.
        public static DataTable GetFemaleDriversCyberjaya()
        {
            string query = @"
        SELECT s.FName, s.LName, s.Phone, s.DateOfBirth
        FROM Staff s
        INNER JOIN Driver d ON s.StaffID = d.StaffID
        INNER JOIN Office o ON s.OfficeID = o.OfficeID
        WHERE s.Gender = 'F' AND o.OfficeName LIKE '%Cyberjaya%'
        ORDER BY s.FName, s.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // c) The total number of staff at each office.
        public static DataTable GetStaffCountPerOffice()
        {
            string query = @"
                SELECT o.OfficeID, o.OfficeName, o.City, COUNT(s.StaffID) AS StaffCount
                FROM Office o
                LEFT JOIN Staff s ON o.OfficeID = s.OfficeID
                GROUP BY o.OfficeID, o.OfficeName, o.City
                ORDER BY o.OfficeName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // d) The details of all taxis at the Cyberjaya office.
        public static DataTable GetTaxisCyberjaya()
        {
            string query = @"
        SELECT t.*, ow.FName + ' ' + ow.LName AS OwnerName,
               (SELECT COUNT(*) FROM TaxiDriverAllocation a WHERE a.TaxiID = t.TaxiID) AS DriverCount
        FROM Taxi t
        INNER JOIN Office o ON t.OfficeID = o.OfficeID
        LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
        WHERE o.OfficeName LIKE '%Cyberjaya%'
        ORDER BY t.PlateNo";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // e) The total number of registered taxis with the company.
        public static DataTable GetTotalRegisteredTaxis()
        {
            string query = @"
                SELECT COUNT(*) AS TotalTaxis FROM Taxi";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // f) The number of drivers allocated to each taxi.
        public static DataTable GetDriverCountPerTaxi()
        {
            string query = @"
                SELECT t.TaxiID, t.PlateNo, t.Model, o.OfficeName,
                       COUNT(a.DriverID) AS DriverCount
                FROM Taxi t
                LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                LEFT JOIN TaxiDriverAllocation a ON t.TaxiID = a.TaxiID
                GROUP BY t.TaxiID, t.PlateNo, t.Model, o.OfficeName
                ORDER BY DriverCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // g) The name and number of owners with more than one taxi.
        public static DataTable GetOwnersWithMultipleTaxis()
        {
            string query = @"
                SELECT o.OwnerID, o.FName, o.LName, o.Phone, COUNT(t.TaxiID) AS TaxiCount
                FROM Owner o
                INNER JOIN Taxi t ON o.OwnerID = t.OwnerID
                GROUP BY o.OwnerID, o.FName, o.LName, o.Phone
                HAVING COUNT(t.TaxiID) > 1
                ORDER BY TaxiCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // h) The full address of all business clients in Cyberjaya.
        public static DataTable GetBusinessClientsCyberjaya()
        {
            string query = @"
        SELECT c.ClientID, c.FName, c.LName, c.Address, c.City, c.Phone, b.CompanyName
        FROM Client c
        INNER JOIN BusinessClient b ON c.ClientID = b.ClientID
        WHERE c.City LIKE '%Cyberjaya%'
        ORDER BY b.CompanyName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // i) The details of the current contracts with business clients in Kuala Lumpur.
        public static DataTable GetCurrentContractsKualaLumpur()
        {
            string query = @"
                SELECT co.*, c.FName + ' ' + c.LName AS ClientName, b.CompanyName, c.Address, c.Phone
                FROM Contract co
                INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                INNER JOIN Client c ON b.ClientID = c.ClientID
                WHERE c.City = 'Kuala Lumpur' 
                AND (co.EndDate IS NULL OR co.EndDate >= GETDATE())
                ORDER BY co.StartDate DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // j) The total number of private clients in each city.
        public static DataTable GetPrivateClientCountByCity()
        {
            string query = @"
                SELECT c.City, COUNT(c.ClientID) AS ClientCount
                FROM Client c
                INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                WHERE c.City IS NOT NULL AND c.City != ''
                GROUP BY c.City
                ORDER BY ClientCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // k) The details of jobs undertaken by a driver on a given day.
        public static DataTable GetJobsByDriverAndDate(int driverId, DateTime jobDate)
        {
            string query = @"
        SELECT j.JobID, j.PickupDateTime, j.PickupAddress, j.DropoffAddress, 
               j.Status, j.MileageKm, j.ChargeAmount, j.FailReason,
               c.FName + ' ' + c.LName AS ClientName,
               t.PlateNo, t.Model
        FROM Job j
        INNER JOIN Client c ON j.ClientID = c.ClientID
        INNER JOIN Taxi t ON j.TaxiID = t.TaxiID
        WHERE j.DriverID = @DriverID 
        AND YEAR(j.PickupDateTime) = @Year 
        AND MONTH(j.PickupDateTime) = @Month 
        AND DAY(j.PickupDateTime) = @Day
        ORDER BY j.PickupDateTime";

            SqlParameter[] parameters = {
        new SqlParameter("@DriverID", driverId),
        new SqlParameter("@Year", jobDate.Year),
        new SqlParameter("@Month", jobDate.Month),
        new SqlParameter("@Day", jobDate.Day)
    };

            DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

            //if (dt != null)
            //{
            //    MessageBox.Show($"Driver: {driverId}, Date: {jobDate.Year}-{jobDate.Month}-{jobDate.Day}, Rows: {dt.Rows.Count}");
            //}

            return dt;
        }
        // l) The names of drivers who are over 25 years old.
        public static DataTable GetDriversOver25()
        {
            string query = @"
                SELECT s.StaffID, s.FName, s.LName, s.DateOfBirth, s.Phone, o.OfficeName,
                       DATEDIFF(YEAR, s.DateOfBirth, GETDATE()) AS Age
                FROM Staff s
                INNER JOIN Driver d ON s.StaffID = d.StaffID
                LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                WHERE DATEDIFF(YEAR, s.DateOfBirth, GETDATE()) > 25
                ORDER BY s.FName, s.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // m) The names and numbers of private clients who hired a taxi in November 2025.
        public static DataTable GetPrivateClientsHiredNov2025()
        {
            string query = @"
                SELECT DISTINCT c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City
                FROM Client c
                INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                INNER JOIN Job j ON c.ClientID = j.ClientID
                WHERE YEAR(j.PickupDateTime) = 2025 AND MONTH(j.PickupDateTime) = 11
                ORDER BY c.FName, c.LName";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // n) The names and addresses of private clients who have hired a taxi more than three times.
        public static DataTable GetPrivateClientsWithMoreThan3Hires()
        {
            string query = @"
                SELECT c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City, COUNT(j.JobID) AS TotalHires
                FROM Client c
                INNER JOIN PrivateClient p ON c.ClientID = p.ClientID
                INNER JOIN Job j ON c.ClientID = j.ClientID
                GROUP BY c.ClientID, c.FName, c.LName, c.Phone, c.Address, c.City
                HAVING COUNT(j.JobID) > 3
                ORDER BY TotalHires DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // o) The average fee charged for a job for private clients.
        public static DataTable GetAverageFeePrivateClients()
        {
            string query = @"
                SELECT AVG(j.ChargeAmount) AS AverageFee,
                       COUNT(j.JobID) AS TotalJobs,
                       SUM(j.ChargeAmount) AS TotalRevenue
                FROM Job j
                INNER JOIN PrivateClient p ON j.ClientID = p.ClientID
                WHERE j.Status = 'Completed' AND j.ChargeAmount IS NOT NULL";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // p) The total number of jobs allocated to each car.
        public static DataTable GetJobCountPerTaxi()
        {
            string query = @"
                SELECT t.TaxiID, t.PlateNo, t.Model, o.OfficeName, COUNT(j.JobID) AS JobCount
                FROM Taxi t
                LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                LEFT JOIN Job j ON t.TaxiID = j.TaxiID
                GROUP BY t.TaxiID, t.PlateNo, t.Model, o.OfficeName
                ORDER BY JobCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // q) The total number of jobs allocated to each driver.
        public static DataTable GetJobCountPerDriver()
        {
            string query = @"
                SELECT s.StaffID, s.FName + ' ' + s.LName AS DriverName, 
                       s.Phone, o.OfficeName, COUNT(j.JobID) AS JobCount
                FROM Driver d
                INNER JOIN Staff s ON d.StaffID = s.StaffID
                LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                LEFT JOIN Job j ON d.StaffID = j.DriverID
                GROUP BY s.StaffID, s.FName, s.LName, s.Phone, o.OfficeName
                ORDER BY JobCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // r) The total amount charged for each car in November 2025.
        public static DataTable GetTotalChargedPerCarNov2025()
        {
            string query = @"
                SELECT t.TaxiID, t.PlateNo, t.Model, o.OfficeName,
                       COUNT(j.JobID) AS TotalJobs,
                       ISNULL(SUM(j.ChargeAmount), 0) AS TotalCharged
                FROM Taxi t
                LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                LEFT JOIN Job j ON t.TaxiID = j.TaxiID
                    AND YEAR(j.PickupDateTime) = 2025 
                    AND MONTH(j.PickupDateTime) = 11
                    AND j.Status = 'Completed'
                GROUP BY t.TaxiID, t.PlateNo, t.Model, o.OfficeName
                ORDER BY TotalCharged DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // s) The total number of jobs and kilometres (km) driven for a given contract for business clients.
        public static DataTable GetContractJobSummary(int contractId)
        {
            string query = @"
                SELECT co.ContractID, co.StartDate, co.EndDate, co.AgreedNumJobs, co.FixedFee,
                       c.FName + ' ' + c.LName AS ClientName, b.CompanyName,
                       COUNT(j.JobID) AS TotalJobs,
                       ISNULL(SUM(j.MileageKm), 0) AS TotalKm,
                       ISNULL(SUM(j.ChargeAmount), 0) AS TotalCharged,
                       CASE 
                           WHEN co.AgreedNumJobs > 0 
                           THEN (COUNT(j.JobID) * 100.0 / co.AgreedNumJobs)
                           ELSE 0 
                       END AS CompletionPercentage
                FROM Contract co
                INNER JOIN BusinessClient b ON co.ClientID = b.ClientID
                INNER JOIN Client c ON b.ClientID = c.ClientID
                LEFT JOIN Job j ON co.ContractID = j.ContractID
                WHERE co.ContractID = @ContractID
                GROUP BY co.ContractID, co.StartDate, co.EndDate, co.AgreedNumJobs, co.FixedFee, 
                         c.FName, c.LName, b.CompanyName";

            SqlParameter[] parameters = {
                new SqlParameter("@ContractID", contractId)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // t) The details of taxi due for maintenance service on January 1, 2026.
        public static DataTable GetTaxisDueForMaintenanceJan2026()
        {
            string query = @"
                SELECT t.*, o.OfficeName, ow.FName + ' ' + ow.LName AS OwnerName,
                       (SELECT COUNT(*) FROM TaxiDriverAllocation a WHERE a.TaxiID = t.TaxiID) AS DriverCount
                FROM Taxi t
                LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                LEFT JOIN Owner ow ON t.OwnerID = ow.OwnerID
                WHERE t.NextServiceDate <= '2026-01-01'
                ORDER BY t.NextServiceDate";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Nested Query 1: Find drivers who have driven more than the average number of jobs
        public static DataTable GetDriversAboveAverageJobs()
        {
            string query = @"
                SELECT s.StaffID, s.FName + ' ' + s.LName AS DriverName, 
                       COUNT(j.JobID) AS JobCount,
                       (SELECT AVG(JobCount) FROM 
                           (SELECT DriverID, COUNT(*) AS JobCount 
                            FROM Job GROUP BY DriverID) AS AvgJobs) AS AverageJobs
                FROM Driver d
                INNER JOIN Staff s ON d.StaffID = s.StaffID
                LEFT JOIN Job j ON d.StaffID = j.DriverID
                GROUP BY s.StaffID, s.FName, s.LName
                HAVING COUNT(j.JobID) > 
                    (SELECT AVG(JobCount) FROM 
                        (SELECT DriverID, COUNT(*) AS JobCount 
                         FROM Job GROUP BY DriverID) AS AvgJobs)
                ORDER BY JobCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Nested Query 2: Find taxis that have never been used
        public static DataTable GetTaxisNeverUsed()
        {
            string query = @"
                SELECT t.TaxiID, t.PlateNo, t.Model, o.OfficeName
                FROM Taxi t
                LEFT JOIN Office o ON t.OfficeID = o.OfficeID
                WHERE t.TaxiID NOT IN (SELECT DISTINCT TaxiID FROM Job WHERE TaxiID IS NOT NULL)
                ORDER BY t.PlateNo";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Nested Query 3: Find clients who have spent more than the average client
        public static DataTable GetClientsAboveAverageSpending()
        {
            string query = @"
                SELECT c.ClientID, c.FName + ' ' + c.LName AS ClientName, 
                       c.Phone, SUM(j.ChargeAmount) AS TotalSpent,
                       (SELECT AVG(TotalSpent) FROM 
                           (SELECT ClientID, SUM(ChargeAmount) AS TotalSpent 
                            FROM Job WHERE Status = 'Completed' 
                            GROUP BY ClientID) AS AvgSpending) AS AverageSpending
                FROM Client c
                INNER JOIN Job j ON c.ClientID = j.ClientID
                WHERE j.Status = 'Completed'
                GROUP BY c.ClientID, c.FName, c.LName, c.Phone
                HAVING SUM(j.ChargeAmount) > 
                    (SELECT AVG(TotalSpent) FROM 
                        (SELECT ClientID, SUM(ChargeAmount) AS TotalSpent 
                         FROM Job WHERE Status = 'Completed' 
                         GROUP BY ClientID) AS AvgSpending)
                ORDER BY TotalSpent DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Nested Query 4: Find offices with more staff than average
        public static DataTable GetOfficesAboveAverageStaff()
        {
            string query = @"
                SELECT o.OfficeID, o.OfficeName, o.City, COUNT(s.StaffID) AS StaffCount,
                       (SELECT AVG(StaffCount) FROM 
                           (SELECT OfficeID, COUNT(*) AS StaffCount 
                            FROM Staff GROUP BY OfficeID) AS AvgStaff) AS AverageStaff
                FROM Office o
                LEFT JOIN Staff s ON o.OfficeID = s.OfficeID
                GROUP BY o.OfficeID, o.OfficeName, o.City
                HAVING COUNT(s.StaffID) > 
                    (SELECT AVG(StaffCount) FROM 
                        (SELECT OfficeID, COUNT(*) AS StaffCount 
                         FROM Staff GROUP BY OfficeID) AS AvgStaff)
                ORDER BY StaffCount DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Nested Query 5: Find owners whose taxis have generated more revenue than average
        public static DataTable GetOwnersAboveAverageRevenue()
        {
            string query = @"
                SELECT ow.OwnerID, ow.FName + ' ' + ow.LName AS OwnerName, 
                       ow.Phone, ISNULL(SUM(j.ChargeAmount), 0) AS TotalRevenue,
                       (SELECT AVG(Revenue) FROM 
                           (SELECT t.OwnerID, SUM(j.ChargeAmount) AS Revenue
                            FROM Taxi t
                            LEFT JOIN Job j ON t.TaxiID = j.TaxiID AND j.Status = 'Completed'
                            GROUP BY t.OwnerID) AS AvgRev) AS AverageRevenue
                FROM Owner ow
                LEFT JOIN Taxi t ON ow.OwnerID = t.OwnerID
                LEFT JOIN Job j ON t.TaxiID = j.TaxiID AND j.Status = 'Completed'
                GROUP BY ow.OwnerID, ow.FName, ow.LName, ow.Phone
                HAVING ISNULL(SUM(j.ChargeAmount), 0) > 
                    (SELECT AVG(Revenue) FROM 
                        (SELECT t.OwnerID, SUM(j.ChargeAmount) AS Revenue
                         FROM Taxi t
                         LEFT JOIN Job j ON t.TaxiID = j.TaxiID AND j.Status = 'Completed'
                         GROUP BY t.OwnerID) AS AvgRev)
                ORDER BY TotalRevenue DESC";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Dashboard Summary Statistics
        public static DataTable GetDashboardSummary()
        {
            string query = @"
                SELECT 
                    (SELECT COUNT(*) FROM Office) AS TotalOffices,
                    (SELECT COUNT(*) FROM Staff) AS TotalStaff,
                    (SELECT COUNT(*) FROM Driver) AS TotalDrivers,
                    (SELECT COUNT(*) FROM Owner) AS TotalOwners,
                    (SELECT COUNT(*) FROM Taxi) AS TotalTaxis,
                    (SELECT COUNT(*) FROM Client) AS TotalClients,
                    (SELECT COUNT(*) FROM PrivateClient) AS TotalPrivateClients,
                    (SELECT COUNT(*) FROM BusinessClient) AS TotalBusinessClients,
                    (SELECT COUNT(*) FROM Contract WHERE EndDate IS NULL OR EndDate >= GETDATE()) AS ActiveContracts,
                    (SELECT COUNT(*) FROM Job WHERE CAST(PickupDateTime AS DATE) = CAST(GETDATE() AS DATE)) AS TodayJobs,
                    (SELECT COUNT(*) FROM Job WHERE Status = 'Completed') AS CompletedJobs,
                    (SELECT COUNT(*) FROM Job WHERE Status = 'Failed') AS FailedJobs,
                    (SELECT ISNULL(SUM(ChargeAmount), 0) FROM Job WHERE Status = 'Completed') AS TotalRevenue,
                    (SELECT ISNULL(AVG(ChargeAmount), 0) FROM Job WHERE Status = 'Completed') AS AverageJobFee";

            return DatabaseHelper.ExecuteQuery(query);
        }

        // Monthly Revenue Report
        public static DataTable GetMonthlyRevenue(int year)
        {
            string query = @"
                SELECT 
                    MONTH(j.PickupDateTime) AS Month,
                    COUNT(j.JobID) AS TotalJobs,
                    ISNULL(SUM(j.ChargeAmount), 0) AS Revenue
                FROM Job j
                WHERE YEAR(j.PickupDateTime) = @Year AND j.Status = 'Completed'
                GROUP BY MONTH(j.PickupDateTime)
                ORDER BY Month";

            SqlParameter[] parameters = {
                new SqlParameter("@Year", year)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }

        // Top Performing Drivers
        public static DataTable GetTopDrivers(int topCount)
        {
            string query = @"
                SELECT TOP (@TopCount) 
                    s.StaffID, s.FName + ' ' + s.LName AS DriverName,
                    o.OfficeName, COUNT(j.JobID) AS JobCount,
                    ISNULL(SUM(j.ChargeAmount), 0) AS TotalRevenue
                FROM Driver d
                INNER JOIN Staff s ON d.StaffID = s.StaffID
                LEFT JOIN Office o ON s.OfficeID = o.OfficeID
                LEFT JOIN Job j ON d.StaffID = j.DriverID AND j.Status = 'Completed'
                GROUP BY s.StaffID, s.FName, s.LName, o.OfficeName
                ORDER BY TotalRevenue DESC, JobCount DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@TopCount", topCount)
            };

            return DatabaseHelper.ExecuteQuery(query, parameters);
        }
    }
}