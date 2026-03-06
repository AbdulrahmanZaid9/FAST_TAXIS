using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAST_TAXIS3.Helpers
{
    public static class Constants
    {
        // Job Status
        public const string JOB_STATUS_COMPLETED = "Completed";
        public const string JOB_STATUS_FAILED = "Failed";
        public const string JOB_STATUS_PENDING = "Pending";

        // Staff Types
        public const string STAFF_TYPE_MANAGER = "Manager";
        public const string STAFF_TYPE_ADMIN = "Admin";
        public const string STAFF_TYPE_DRIVER = "Driver";

        // Client Types
        public const string CLIENT_TYPE_PRIVATE = "Private";
        public const string CLIENT_TYPE_BUSINESS = "Business";

        // Gender
        public const string GENDER_MALE = "M";
        public const string GENDER_FEMALE = "F";

        // Office Cities
        public const string CITY_CYBERJAYA = "Cyberjaya";
        public const string CITY_KUALA_LUMPUR = "Kuala Lumpur";
        public const string CITY_SELANGOR = "Selangor";

        // Error Messages
        public const string ERROR_CONNECTION = "Failed to connect to database.";
        public const string ERROR_INVALID_DATA = "Invalid data provided.";
        public const string ERROR_REQUIRED_FIELD = "This field is required.";
        public const string ERROR_PHONE_FORMAT = "Invalid phone number format.";
        public const string ERROR_EMAIL_FORMAT = "Invalid email format.";

        // Success Messages
        public const string SUCCESS_SAVED = "Record saved successfully.";
        public const string SUCCESS_UPDATED = "Record updated successfully.";
        public const string SUCCESS_DELETED = "Record deleted successfully.";

        // Date Formats
        public const string DATE_FORMAT_DISPLAY = "dd/MM/yyyy";
        public const string DATETIME_FORMAT_DISPLAY = "dd/MM/yyyy HH:mm";
    }
}