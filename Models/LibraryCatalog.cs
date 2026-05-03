using System;
using System.Collections.Generic;
using System.Linq;
using LibrarySystem.Events;
using LibrarySystem.Models;
using LibrarySystem.Utilities;

namespace LibrarySystem.Models
{
    // فئة الكتالوج تحتوي على قائمة من كائنات LibraryItemBase لإظهار تعدد الأشكال
    public class LibraryCatalog
    {
        // خاصية ساكنة لحساب إجمالي عدد العناصر في جميع الكتالوجات
        private static int _totalItemsCreated = 0;
        public static int TotalItemsCreated => _totalItemsCreated;

        // الحقول الخاصة
        private readonly string _catalogName;
        private List<LibraryItemBase> _items;

        // تعريف الأحداث باستخدام التفويضات
        public event ItemBorrowedHandler OnItemBorrowed;
        public event ItemReturnedHandler OnItemReturned;
        public event BorrowLimitReachedHandler OnBorrowLimitReached;

        // سجل الاستعارات: المفتاح = itemId، القيمة = اسم المستعير
        private Dictionary<string, (string borrowerName, DateTime borrowDate)> _borrowRecords;

        public string CatalogName => _catalogName;

        public int ItemCount => _items.Count;

        public LibraryCatalog(string name)
        {
            _catalogName = name;
            _items = new List<LibraryItemBase>();
            _borrowRecords = new Dictionary<string, (string, DateTime)>();
        }

        // إضافة عنصر للكتالوج
        public void AddItem(LibraryItemBase item)
        {
            if (item == null) return;
            _items.Add(item);
            _totalItemsCreated++;
            Console.WriteLine($"  ✓ تم إضافة: {item.Title}");
        }

        // عرض جميع العناصر - هنا يظهر تعدد الأشكال Polymorphism
        // نستدعي GetInfo() على كل عنصر وكل نوع يستجيب بطريقته الخاصة
        public void DisplayAllItems()
        {
            Console.WriteLine($"\n=== كتالوج: {_catalogName} ({_items.Count} عنصر) ===");
            if (_items.Count == 0)
            {
                Console.WriteLine("  الكتالوج فارغ.");
                return;
            }

            foreach (var item in _items)
            {
                // كل كائن يستدعي نسخته الخاصة من GetInfo() - هذا هو تعدد الأشكال
                Console.WriteLine($"  {(item.IsAvailable() ? "✓" : "✗")} {item.GetInfo()}");
            }
        }

        // استعارة عنصر من الكتالوج - يطلق حدثًا عند النجاح
        public bool BorrowItem(string itemId, string borrowerName)
        {
            if (!Validator.IsValidName(borrowerName))
            {
                Console.WriteLine(Validator.GetValidationError("اسم المستعير"));
                return false;
            }

            // التحقق من حد الاستعارة لنفس الشخص
            int currentBorrowCount = _borrowRecords.Values.Count(r => r.borrowerName == borrowerName);
            if (!Validator.IsWithinBorrowLimit(currentBorrowCount))
            {
                // إطلاق حدث تجاوز الحد
                OnBorrowLimitReached?.Invoke(borrowerName, currentBorrowCount, 5);
                return false;
            }

            var item = _items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                Console.WriteLine($"  ✗ العنصر {itemId} غير موجود.");
                return false;
            }

            if (!item.IsAvailable())
            {
                Console.WriteLine($"  ✗ العنصر '{item.Title}' غير متاح حاليًا.");
                return false;
            }

            // تحديث الحالة
            item.SetAvailable(false);
            _borrowRecords[itemId] = (borrowerName, DateTime.Now);

            // إطلاق حدث الاستعارة
            OnItemBorrowed?.Invoke(itemId, borrowerName, item.Title);

            return true;
        }

        // إعادة عنصر - يحسب الغرامة ويطلق حدثًا
        public bool ReturnItem(string itemId, int daysLate = 0)
        {
            var item = _items.FirstOrDefault(i => i.ItemId == itemId);
            if (item == null)
            {
                Console.WriteLine($"  ✗ العنصر {itemId} غير موجود.");
                return false;
            }

            if (item.IsAvailable())
            {
                Console.WriteLine($"  ✗ العنصر '{item.Title}' لم يُستعر أصلًا.");
                return false;
            }

            string borrowerName = _borrowRecords.ContainsKey(itemId)
                ? _borrowRecords[itemId].borrowerName
                : "غير معروف";

            // حساب الغرامة باستخدام تعدد الأشكال - كل نوع له حسابه الخاص
            double fine = Validator.IsValidDaysLate(daysLate) ? item.CalculateFine(daysLate) : 0;

            // تحديث الحالة
            item.SetAvailable(true);
            _borrowRecords.Remove(itemId);

            // إطلاق حدث الإعادة
            OnItemReturned?.Invoke(itemId, borrowerName, fine);

            return true;
        }

        // البحث عن عنصر بالعنوان
        public LibraryItemBase SearchByTitle(string title)
        {
            return _items.FirstOrDefault(i =>
                i.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }

        // عرض العناصر المتاحة فقط
        public void DisplayAvailableItems()
        {
            Console.WriteLine($"\n=== العناصر المتاحة في: {_catalogName} ===");
            var available = _items.Where(i => i.IsAvailable()).ToList();
            if (available.Count == 0)
            {
                Console.WriteLine("  لا توجد عناصر متاحة حاليًا.");
                return;
            }
            foreach (var item in available)
                Console.WriteLine($"  ✓ {item.GetInfo()}");
        }
    }
}
