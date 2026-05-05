using System;
using System.Text.RegularExpressions;

namespace LibrarySystem.Utilities
{
    // فئة ساكنة تحتوي على دوال التحقق من صحة البيانات
    public static class Validator
    {
        // التحقق من أن الاسم غير فارغ وبطول مناسب
        public static bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.Trim().Length >= 2;
        }

        // التحقق من سنة النشر
        public static bool IsValidYear(int year)
        {
            return year >= 1800 && year <= DateTime.Now.Year;
        }

        // التحقق من صحة رقم ISBN (13 رقمًا)
        public static bool IsValidISBN(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return false;
            string cleaned = isbn.Replace("-", "").Replace(" ", "");
            return cleaned.Length == 13 && Regex.IsMatch(cleaned, @"^\d{13}$");
        }

        // التحقق من أن عدد أيام التأخير إيجابي
        public static bool IsValidDaysLate(int days)
        {
            return days >= 0;
        }

        // التحقق من حد الاستعارة (لا يتجاوز 5 عناصر)
        public static bool IsWithinBorrowLimit(int currentCount, int maxAllowed = 5)
        {
            return currentCount < maxAllowed;
        }

        // رسالة خطأ موحدة
        public static string GetValidationError(string fieldName)
        {
            return $"خطأ: قيمة الحقل '{fieldName}' غير صحيحة.";
        }
    }
}
