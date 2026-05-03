using System;
using LibrarySystem.Interfaces;

namespace LibrarySystem.Models
{
    // الفئة المجردة الأساسية التي تطبق الواجهة وتضم الخصائص المشتركة
    public abstract class LibraryItemBase : ILibraryItem
    {
        // حقول خاصة (Encapsulation)
        private readonly string _itemId;   // المعرف الفريد لا يمكن تعديله بعد الإنشاء
        private string _title;
        private string _author;
        private int _yearPublished;
        private bool _isAvailable;

        // عداد ساكن لتوليد معرفات فريدة
        private static int _idCounter = 1;

        // Constructor
        protected LibraryItemBase(string title, string author, int yearPublished)
        {
            _itemId = "LIB-" + _idCounter++;   // معرف فريد لا يتغير
            _title = title;
            _author = author;
            _yearPublished = yearPublished;
            _isAvailable = true;
        }

        // خصائص للوصول إلى الحقول الخاصة
        public string ItemId => _itemId;    // للقراءة فقط

        public string Title
        {
            get => _title;
            protected set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _title = value;
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _author = value;
            }
        }

        public int YearPublished
        {
            get => _yearPublished;
            set
            {
                if (value > 0 && value <= DateTime.Now.Year)
                    _yearPublished = value;
            }
        }

        public bool AvailabilityStatus
        {
            get => _isAvailable;
            protected set => _isAvailable = value;
        }

        // تنفيذ طرق الواجهة
        public bool IsAvailable() => _isAvailable;

        // تحديد حالة التوفر داخليًا عبر دوال مناسبة
        public void SetAvailable(bool status) => _isAvailable = status;

        // طريقة مجردة يجب على كل فئة فرعية تنفيذها بطريقتها
        public abstract string GetInfo();

        // طريقة مجردة لحساب الغرامة
        public abstract double CalculateFine(int daysLate);

        // طريقة مشتركة يمكن تجاوزها
        public virtual void DisplayDetails()
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"  المعرف    : {_itemId}");
            Console.WriteLine($"  العنوان   : {_title}");
            Console.WriteLine($"  المؤلف    : {_author}");
            Console.WriteLine($"  سنة النشر : {_yearPublished}");
            Console.WriteLine($"  الحالة    : {(_isAvailable ? "متاح ✓" : "مستعار ✗")}");
        }
    }
}
