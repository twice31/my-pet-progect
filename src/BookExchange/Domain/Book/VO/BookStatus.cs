namespace Domain.Book.VO
{
    // Перечисление для Статуса книги
    public enum BookStatus
    {
        Available,
        Reserved,
        Exchanged,
        NotAvailable
    }

    // Объект значения для Статуса книги
    public record BookStatusValue
    {
        public BookStatus Status { get; }

        public BookStatusValue(BookStatus status)
        {
            Status = status;
        }

        // Переопределяем метод ToString для вывода в виде строки
        public override string ToString() => Status.ToString();
    }
}
