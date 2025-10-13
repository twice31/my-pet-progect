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

        // Закрытый конструктор
        private BookStatusValue(BookStatus status)
        {
            Status = status;
        }

        // Фабричный метод
        public static BookStatusValue Create(BookStatus status)
        {
            return new BookStatusValue(status);
        }

        public override string ToString()
        {
            return Status switch
            {
                BookStatus.Available => "Доступна",
                BookStatus.Reserved => "Зарезервирована",
                BookStatus.Exchanged => "Завершён обмен",
                BookStatus.NotAvailable => "Недоступна",
                _ => "Неизвестный статус"
            };
        }
    }
}
