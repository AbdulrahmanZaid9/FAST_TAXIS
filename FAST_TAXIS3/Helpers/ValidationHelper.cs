using System;
using System.Text.RegularExpressions;

namespace FAST_TAXIS3.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsNotEmpty(string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string pattern = @"^[0-9\+\-\s\(\)]{10,15}$";
            return Regex.IsMatch(phone, pattern);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsValidAge(DateTime dateOfBirth, int minimumAge = 18)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Now.AddYears(-age))
                age--;

            return age >= minimumAge;
        }

        public static bool IsPositiveNumber(int number)
        {
            return number > 0;
        }

        public static bool IsPositiveNumber(decimal number)
        {
            return number > 0;
        }

        public static bool IsValidDate(DateTime date)
        {
            return date != null && date >= DateTime.MinValue;
        }

        public static bool IsFutureDate(DateTime date)
        {
            return date.Date >= DateTime.Now.Date;
        }

        public static bool IsValidPlateNumber(string plateNo)
        {
            if (string.IsNullOrWhiteSpace(plateNo))
                return false;

            string pattern = @"^[A-Z0-9\s\-]{5,10}$";
            return Regex.IsMatch(plateNo.ToUpper(), pattern);
        }

        public static bool IsValidLicenseNumber(string licenseNo)
        {
            if (string.IsNullOrWhiteSpace(licenseNo))
                return false;

            string pattern = @"^[A-Z0-9\s\-]{8,15}$";
            return Regex.IsMatch(licenseNo.ToUpper(), pattern);
        }

        public static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }

        public static bool IsDecimal(string value)
        {
            return decimal.TryParse(value, out _);
        }
    }
}