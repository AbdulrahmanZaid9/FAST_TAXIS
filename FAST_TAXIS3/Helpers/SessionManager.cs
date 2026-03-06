using System;
using System.Collections.Generic;

namespace FAST_TAXIS3.Helpers
{
    public static class SessionManager
    {
        public static int CurrentStaffID { get; set; }
        public static string CurrentStaffName { get; set; }
        public static string CurrentStaffRole { get; set; }
        public static int CurrentOfficeID { get; set; }
        public static string CurrentOfficeName { get; set; }
        public static DateTime LoginTime { get; set; }
        public static bool IsLoggedIn { get; private set; }

        private static Dictionary<string, bool> _permissions = new Dictionary<string, bool>();

        public static void Login(int staffId, string name, string role, int officeId, string officeName)
        {
            CurrentStaffID = staffId;
            CurrentStaffName = name;
            CurrentStaffRole = role;
            CurrentOfficeID = officeId;
            CurrentOfficeName = officeName;
            LoginTime = DateTime.Now;
            IsLoggedIn = true;

            LoadPermissions(role);
        }

        public static void Logout()
        {
            CurrentStaffID = 0;
            CurrentStaffName = null;
            CurrentStaffRole = null;
            CurrentOfficeID = 0;
            CurrentOfficeName = null;
            IsLoggedIn = false;
            _permissions.Clear();
        }

        private static void LoadPermissions(string role)
        {
            _permissions.Clear();

            switch (role)
            {
                // ========================
                // Director - كل الصلاحيات
                // ========================
                case "Director":
                    _permissions.Add("CanViewOffices", true);
                    _permissions.Add("CanManageOffices", true);
                    _permissions.Add("CanViewStaff", true);
                    _permissions.Add("CanManageStaff", true);
                    _permissions.Add("CanViewOwners", true);
                    _permissions.Add("CanManageOwners", true);
                    _permissions.Add("CanViewTaxis", true);
                    _permissions.Add("CanManageTaxis", true);
                    _permissions.Add("CanViewClients", true);
                    _permissions.Add("CanManageClients", true);
                    _permissions.Add("CanViewContracts", true);
                    _permissions.Add("CanManageContracts", true);
                    _permissions.Add("CanViewJobs", true);
                    _permissions.Add("CanManageJobs", true);
                    _permissions.Add("CanViewReports", true);
                    _permissions.Add("CanViewAllOffices", true);
                    break;

                // ========================
                // Admin - كل الصلاحيات
                // ========================
                case "Admin":
                    _permissions.Add("CanViewOffices", true);
                    _permissions.Add("CanManageOffices", true);
                    _permissions.Add("CanViewStaff", true);
                    _permissions.Add("CanManageStaff", true);
                    _permissions.Add("CanViewOwners", true);
                    _permissions.Add("CanManageOwners", true);
                    _permissions.Add("CanViewTaxis", true);
                    _permissions.Add("CanManageTaxis", true);
                    _permissions.Add("CanViewClients", true);
                    _permissions.Add("CanManageClients", true);
                    _permissions.Add("CanViewContracts", true);
                    _permissions.Add("CanManageContracts", true);
                    _permissions.Add("CanViewJobs", true);
                    _permissions.Add("CanManageJobs", true);
                    _permissions.Add("CanViewReports", true);
                    _permissions.Add("CanViewAllOffices", true);
                    break;

                // ========================
                // Manager - مكتبه فقط
                // ========================
                case "Manager":
                    _permissions.Add("CanViewOffices", false);
                    _permissions.Add("CanManageOffices", false);
                    _permissions.Add("CanViewStaff", true);
                    _permissions.Add("CanManageStaff", true);
                    _permissions.Add("CanViewOwners", true);
                    _permissions.Add("CanManageOwners", true);
                    _permissions.Add("CanViewTaxis", true);
                    _permissions.Add("CanManageTaxis", true);
                    _permissions.Add("CanViewClients", true);
                    _permissions.Add("CanManageClients", true);
                    _permissions.Add("CanViewContracts", true);
                    _permissions.Add("CanManageContracts", true);
                    _permissions.Add("CanViewJobs", true);
                    _permissions.Add("CanManageJobs", true);
                    _permissions.Add("CanViewReports", true);
                    _permissions.Add("CanViewAllOffices", false);
                    break;

                // ========================
                // Driver - رحلاته فقط
                // ========================
                case "Driver":
                    _permissions.Add("CanViewOffices", false);
                    _permissions.Add("CanManageOffices", false);
                    _permissions.Add("CanViewStaff", false);
                    _permissions.Add("CanManageStaff", false);
                    _permissions.Add("CanViewOwners", false);
                    _permissions.Add("CanManageOwners", false);
                    _permissions.Add("CanViewTaxis", false);
                    _permissions.Add("CanManageTaxis", false);
                    _permissions.Add("CanViewClients", false);
                    _permissions.Add("CanManageClients", false);
                    _permissions.Add("CanViewContracts", false);
                    _permissions.Add("CanManageContracts", false);
                    _permissions.Add("CanViewJobs", true);
                    _permissions.Add("CanManageJobs", true);
                    _permissions.Add("CanViewReports", false);
                    _permissions.Add("CanViewAllOffices", false);
                    break;

                // ========================
                // Staff عادي - عملاء فقط
                // ========================
                default:
                    _permissions.Add("CanViewOffices", false);
                    _permissions.Add("CanManageOffices", false);
                    _permissions.Add("CanViewStaff", false);
                    _permissions.Add("CanManageStaff", false);
                    _permissions.Add("CanViewOwners", false);
                    _permissions.Add("CanManageOwners", false);
                    _permissions.Add("CanViewTaxis", false);
                    _permissions.Add("CanManageTaxis", false);
                    _permissions.Add("CanViewClients", true);
                    _permissions.Add("CanManageClients", true);
                    _permissions.Add("CanViewContracts", false);
                    _permissions.Add("CanManageContracts", false);
                    _permissions.Add("CanViewJobs", false);
                    _permissions.Add("CanManageJobs", false);
                    _permissions.Add("CanViewReports", false);
                    _permissions.Add("CanViewAllOffices", false);
                    break;
            }
        }

        public static bool HasPermission(string permission)
        {
            if (!IsLoggedIn)
                return false;

            return _permissions.ContainsKey(permission) && _permissions[permission];
        }

        public static string GetCurrentUserDisplay()
        {
            if (!IsLoggedIn)
                return "Not Logged In";

            return $"{CurrentStaffName} ({CurrentStaffRole}) - {CurrentOfficeName}";
        }
    }
}