using Domain.Book.VO;
using System;

namespace Domain.Book
{
    public class Book
    {
        public BookId Id { get; }
        public Title BookTitle { get; }
        public Author Author { get; }
        public ISBN Isbn { get; }
        // Заменяем BookStatusValue на BookStatus (Умное перечисление)
        public BookStatus Status { get; private set; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР:
        private Book(BookId id, Title title, Author author, ISBN isbn, BookStatus status)
        {
            Id = id;
            BookTitle = title;
            Author = author;
            Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn), "ISBN книги не может быть null.");
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус книги не может быть null.");
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания существующей книги
        public static Book Create(BookId id, Title title, Author author, ISBN isbn, BookStatus status)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор книги не может быть пустым.");
            if (title == null) throw new ArgumentNullException(nameof(title), "Название книги не может быть пустым.");
            if (author == null) throw new ArgumentNullException(nameof(author), "Автор книги не может быть пустым.");

            return new Book(id, title, author, isbn, status);
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания НОВОЙ книги
        public static Book New(Title title, Author author, string isbnValue)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            if (author == null) throw new ArgumentNullException(nameof(author));

            var isbn = ISBN.Create(isbnValue);
            var id = BookId.Create(Guid.NewGuid());
            // Используем статический экземпляр BookStatus.Available
            var status = BookStatus.Available;

            return Create(id, title, author, isbn, status);
        }


        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Изменить статус книги
        public void UpdateStatus(BookStatus newStatus)
        {
            // Используем оператор сравнения (унаследованный от Enumeration)
            if (Status == newStatus)
            {
                throw new InvalidOperationException("Статус книги уже установлен на выбранное значение.");
            }

            // ИСПОЛЬЗУЕМ ПОВЕДЕНИЕ УМНОГО ПЕРЕЧИСЛЕНИЯ

            if (Status == BookStatus.Available && (newStatus == BookStatus.Reserved || newStatus == BookStatus.Exchanged))
            {
                Status = newStatus;
            }
            else if (Status == BookStatus.Reserved && newStatus == BookStatus.Exchanged)
            {
                Status = newStatus;
            }
            else
            {
                throw new InvalidOperationException($"Невозможно изменить статус книги с '{Status.Name}' на '{newStatus.Name}' в текущем состоянии.");
            }
        }

        // Метод для отображения информации о книге
        public override string ToString()
        {
            // ИСПОЛЬЗУЕМ УМНОЕ ПЕРЕЧИСЛЕНИЕ: Status.Name для понятного названия
            return $"Книга: {BookTitle} Автор: {Author}, ISBN: {Isbn} - Статус: {Status.Name}";
        }
    }
}