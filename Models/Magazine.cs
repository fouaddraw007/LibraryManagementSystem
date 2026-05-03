using System;

namespace LibrarySystem.Models
{
    // فئة المجلة ترث من الفئة المجردة
    public class Magazine : LibraryItemBase
    {
        private int _issueNumber;
        private string _frequency;   // أسبوعية / شهرية / سنوية

        public Magazine(string title, string publisher, int yearPublished, int issueNumber, string frequency)
            : base(title, publisher, yearPublished)
        {
            _issueNumber = issueNumber;
            _frequency = frequency;
        }

        public int IssueNumber
        {
            get => _issueNumber;
            set { if (value > 0) _issueNumber = value; }
        }

        public string Frequency
        {
            get => _frequency;
            set { if (!string.IsNullOrWhiteSpace(value)) _frequency = value; }
        }

        // تجاوز الطريقة المجردة بطريقة خاصة بالمجلة
        public override string GetInfo()
        {
            return $"[مجلة] {Title} | الناشر: {Author} | العدد: {_issueNumber} | التكرار: {_frequency}";
        }

        // المجلات غرامتها أقل: 20 ل.س عن كل يوم
        public override double CalculateFine(int daysLate)
        {
            return daysLate * 20.0;
        }

        public override void DisplayDetails()
        {
            base.DisplayDetails();
            Console.WriteLine($"  رقم العدد : {_issueNumber}");
            Console.WriteLine($"  الدورية   : {_frequency}");
            Console.WriteLine("========================================");
        }
    }
}
