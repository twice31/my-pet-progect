using Domain.Book.VO;
using System;

namespace Domain.Book
{
    public class Book
    {
        public BookId Id { get; }
        public Title BookTitle { get; }
        public Author Author { get; }
        public BookStatusValue Status { get; private set; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР: используется только для инициализации
        private Book(BookId id, Title title, Author author, BookStatusValue status)
        {
            Id = id;
            BookTitle = title;
            Author = author;
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус книги не может быть null.");
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания существующей книги
        public static Book Create(BookId id, Title title, Author author, BookStatusValue status)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор книги не может быть пустым.");
            if (title == null) throw new ArgumentNullException(nameof(title), "Название книги не может быть пустым.");
            if (author == null) throw new ArgumentNullException(nameof(author), "Автор книги не может быть пустым.");
            // Status проверяется в конструкторе

            return new Book(id, title, author, status);
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания НОВОЙ книшги
        public static Book New(Title title, Author author)
        {
            var id = BookId.Create(Guid.NewGuid());
            var status = BookStatusValue.Create(BookStatus.Available);

            return Create(id, title, author, status);
        }

        // МЕТОД, ИЗМЕНЯЮЩИЙ СОСТОЯНИЕ: Изменить статус книги
        public void UpdateStatus(BookStatus newStatus)
        {
            if (Status.Status == newStatus)
            {
                throw new InvalidOperationException("Статус книги уже установлен на выбранное значение.");
            }


            // BookStatusValue — это ValueObject
            if (Status.Status == BookStatus.Available && (newStatus == BookStatus.Reserved || newStatus == BookStatus.Exchanged))
            {
                Status = BookStatusValue.Create(newStatus);
            }
            else if (Status.Status == BookStatus.Reserved && newStatus == BookStatus.Exchanged)
            {
                Status = BookStatusValue.Create(newStatus);
            }
            else
            {
                throw new InvalidOperationException($"Невозможно изменить статус книги с '{Status.Status}' на '{newStatus}' в текущем состоянии.");
            }
        }

        // Метод для отображения информации о книге
        public override string ToString()
        {
            return $"Книга: {BookTitle} Автор: {Author} - Статус: {Status}";
        }
    }
}