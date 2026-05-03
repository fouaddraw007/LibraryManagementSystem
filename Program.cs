using System;
using LibrarySystem.Models;
using LibrarySystem.Utilities;

namespace LibrarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║     نظام إدارة المكتبة الرقمية      ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            // إنشاء الكتالوج
            LibraryCatalog catalog = new LibraryCatalog("المكتبة المركزية");

            // ربط الأحداث بمعالجات (Event Handlers)
            catalog.OnItemBorrowed += (id, name, title) =>
            {
                Console.WriteLine($"\n  📚 [حدث الاستعارة] استعار '{name}' العنصر: '{title}' (ID: {id})");
            };

            catalog.OnItemReturned += (id, name, fine) =>
            {
                Console.WriteLine($"\n  ✅ [حدث الإعادة] أعاد '{name}' العنصر (ID: {id})");
                if (fine > 0)
                    Console.WriteLine($"     💰 الغرامة المستحقة: {fine} ل.س");
                else
                    Console.WriteLine("     🎉 لا توجد غرامة تأخير.");
            };

            catalog.OnBorrowLimitReached += (name, current, max) =>
            {
                Console.WriteLine($"\n  ⚠️  [حدث التجاوز] '{name}' تجاوز الحد المسموح به!");
                Console.WriteLine($"     عدد الاستعارات الحالي: {current} / الحد الأقصى: {max}");
            };

            // ==========================================
            // 1. إضافة عناصر متنوعة (Polymorphism)
            // ==========================================
            Console.WriteLine("--- إضافة العناصر إلى الكتالوج ---");

            // التحقق من صحة البيانات قبل الإضافة (Static Class)
            string bookTitle = "مبادئ البرمجة الغرضية";
            string bookAuthor = "د. أحمد السيد";
            int bookYear = 2020;
            string isbn = "9781234567890";

            if (Validator.IsValidName(bookTitle) &&
                Validator.IsValidName(bookAuthor) &&
                Validator.IsValidYear(bookYear) &&
                Validator.IsValidISBN(isbn))
            {
                catalog.AddItem(new Book(bookTitle, bookAuthor, bookYear, isbn, 350, "تقنية"));
            }

            catalog.AddItem(new Book("الخوارزميات وتحليلها", "د. سامي العمر", 2018,
                "9789876543210", 480, "علوم حاسوب"));

            catalog.AddItem(new Magazine("مجلة العلوم والتقنية", "دار النشر العلمية",
                2023, 45, "شهرية"));

            catalog.AddItem(new Magazine("مجلة البرمجيات", "اتحاد المطورين", 2022, 12, "فصلية"));

            catalog.AddItem(new DVD("عالم الشبكات", "م. خالد رشيد", 2021, 180, "العربية", "G"));

            catalog.AddItem(new DVD("تعلم الذكاء الاصطناعي", "د. ليلى حسين", 2023, 240, "العربية", "G"));

            // ==========================================
            // 2. عرض جميع العناصر - تعدد الأشكال
            // ==========================================
            catalog.DisplayAllItems();

            // ==========================================
            // 3. عمليات الاستعارة - إطلاق الأحداث
            // ==========================================
            Console.WriteLine("\n--- عمليات الاستعارة ---");

            // استعارة ناجحة
            catalog.BorrowItem("LIB-1", "محمد العلي");
            catalog.BorrowItem("LIB-3", "محمد العلي");
            catalog.BorrowItem("LIB-5", "سارة الحسن");

            // محاولة استعارة عنصر غير متاح
            Console.WriteLine("\n  > محاولة استعارة عنصر مستعار:");
            catalog.BorrowItem("LIB-1", "خالد نمر");

            // ==========================================
            // 4. عرض العناصر المتاحة
            // ==========================================
            catalog.DisplayAvailableItems();

            // ==========================================
            // 5. إعادة العناصر مع الغرامات
            // ==========================================
            Console.WriteLine("\n--- عمليات الإعادة ---");
            catalog.ReturnItem("LIB-1", 3);    // تأخير 3 أيام - كتاب: 50 × 3 = 150
            catalog.ReturnItem("LIB-3", 0);    // في الوقت - مجلة: بدون غرامة
            catalog.ReturnItem("LIB-5", 2);    // تأخير 2 أيام - DVD: 100 × 2 = 200

            // ==========================================
            // 6. عرض البحث
            // ==========================================
            Console.WriteLine("\n--- البحث في الكتالوج ---");
            var found = catalog.SearchByTitle("الخوارزميات");
            if (found != null)
            {
                Console.WriteLine("  نتيجة البحث:");
                found.DisplayDetails();
            }

            // ==========================================
            // 7. الخاصية الساكنة
            // ==========================================
            Console.WriteLine($"\n--- إحصائيات النظام ---");
            Console.WriteLine($"  إجمالي العناصر المضافة لجميع الكتالوجات: {LibraryCatalog.TotalItemsCreated}");
            Console.WriteLine($"  عناصر هذا الكتالوج: {catalog.ItemCount}");

            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║         انتهى تشغيل البرنامج        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ReadLine();
        }
    }
}
