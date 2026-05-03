using System;

namespace LibrarySystem.Models
{
    // فئة القرص المضغوط (DVD) ترث من الفئة المجردة
    public class DVD : LibraryItemBase
    {
        private int _durationMinutes;
        private string _language;
        private string _rating;     // تصنيف العمر: G / PG / PG-13 / R

        public DVD(string title, string director, int yearPublished, int durationMinutes, string language, string rating)
            : base(title, director, yearPublished)
        {
            _durationMinutes = durationMinutes;
            _language = language;
            _rating = rating;
        }

        public int DurationMinutes
        {
            get => _durationMinutes;
            set { if (value > 0) _durationMinutes = value; }
        }

        public string Language
        {
            get => _language;
            set { if (!string.IsNullOrWhiteSpace(value)) _language = value; }
        }

        public string Rating => _rating;   // للقراءة فقط

        // تجاوز الطريقة المجردة بطريقة خاصة بـ DVD
        public override string GetInfo()
        {
            return $"[DVD] {Title} | المخرج: {Author} | المدة: {_durationMinutes} دقيقة | اللغة: {_language}";
        }

        // DVDs غرامتها أعلى: 100 ل.س عن كل يوم
        public override double CalculateFine(int daysLate)
        {
            return daysLate * 100.0;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"  المدة (دقيقة): {_durationMinutes}");
            Console.WriteLine($"  اللغة       : {_language}");
            Console.WriteLine($"  التصنيف     : {_rating}");
            Console.WriteLine("========================================");
        }
    }
}
