using System;

namespace LibrarySystem.Models
{
    // فئة الكتاب ترث من الفئة المجردة
    public class Book : LibraryItemBase
    {
        private string _isbn;
        private int _pages;
        private string _genre;

        public Book(string title, string author, int yearPublished, string isbn, int pages, string genre)
            : base(title, author, yearPublished)
        {
            _isbn = isbn;
            _pages = pages;
            _genre = genre;
        }

        public string ISBN => _isbn;     // للقراءة فقط

        public int Pages
        {
            get => _pages;
            set { if (value > 0) _pages = value; }
        }

        public string Genre
        {
            get => _genre;
            set { if (!string.IsNullOrWhiteSpace(value)) _genre = value; }
        }

        // تجاوز الطريقة المجردة بطريقة خاصة بالكتاب
        public override string GetInfo()
        {
            return $"[كتاب] {Title} | المؤلف: {Author} | ISBN: {_isbn} | النوع: {_genre}";
        }

        // حساب الغرامة للكتاب: 50 ل.س عن كل يوم تأخير
        public override double CalculateFine(int daysLate)
        {
            return daysLate * 50.0;
        }

        // تجاوز طريقة العرض لإضافة تفاصيل الكتاب
        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"  ISBN      : {_isbn}");
            Console.WriteLine($"  عدد الصفحات: {_pages}");
            Console.WriteLine($"  النوع الأدبي: {_genre}");
            Console.WriteLine("========================================");
        }
    }
}
