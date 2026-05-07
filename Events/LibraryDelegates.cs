namespace LibrarySystem.Events
{
    // تعريف التفويض (Delegate) للإبلاغ عن حدث الاستعارة
    public delegate void ItemBorrowedHandler(string itemId, string borrowerName, string itemTitle);

    // تعريف التفويض للإبلاغ عن حدث الإعادة
    public delegate void ItemReturnedHandler(string itemId, string borrowerName, double fine);

    // تعريف التفويض للإبلاغ عن تجاوز حد الاستعارة
    public delegate void BorrowLimitReachedHandler(string borrowerName, int currentCount, int maxAllowed);
}
