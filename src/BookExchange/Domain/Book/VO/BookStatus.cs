using Domain.Enumerations;
using Domain.Enumerations;
using System;

namespace Domain.Book.VO
{
    // Класс BookStatus теперь наследует от базового класса Enumeration
    public sealed class BookStatus : Enumeration<BookStatus>
    {
        // Статические поля, представляющие конкретные экземпляры статусов
        public static readonly BookStatus Available = new BookStatus(1, "Доступна");
        public static readonly BookStatus Reserved = new BookStatus(2, "Зарезервирована");
        public static readonly BookStatus Exchanged = new BookStatus(3, "Завершён обмен");
        public static readonly BookStatus NotAvailable = new BookStatus(4, "Недоступна");

        public bool CanBeRequested() => this == Available;

        public bool CanBeCancelled() => this == Reserved;

        // Закрытый конструктор вызывает базовый конструктор
        private BookStatus(int key, string name)
            : base(key, name)
        {
        }
    }
}