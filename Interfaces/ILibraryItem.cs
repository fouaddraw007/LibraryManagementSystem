namespace LibrarySystem.Interfaces
{
    // واجهة تمثل العمليات الأساسية لأي عنصر في المكتبة
    public interface ILibraryItem
    {
        string GetInfo();           // إرجاع معلومات العنصر
        bool IsAvailable();         // التحقق من توفر العنصر
        void DisplayDetails();      // عرض التفاصيل على الشاشة
    }
}
