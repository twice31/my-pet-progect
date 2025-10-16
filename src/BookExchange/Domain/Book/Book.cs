using Domain.Book.VO;
using System;

namespace Domain.Book
{
    public class Book
    {
        public BookId Id { get; }
        public Title BookTitle { get; }
        public Author Author { get; }
        // НОВОЕ ПОЛЕ: ISBN
        public ISBN Isbn { get; }
        public BookStatusValue Status { get; private set; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР: используется только для инициализации
        private Book(BookId id, Title title, Author author, ISBN isbn, BookStatusValue status)
        {
            Id = id;
            BookTitle = title;
            Author = author;
            Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn), "ISBN книги не может быть null."); // Добавлена проверка
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус книги не может быть null.");
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания существующей книги
        public static Book Create(BookId id, Title title, Author author, ISBN isbn, BookStatusValue status)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор книги не может быть пустым.");
            if (title == null) throw new ArgumentNullException(nameof(title), "Название книги не может быть пустым.");
            if (author == null) throw new ArgumentNullException(nameof(author), "Автор книги не может быть пустым.");
            // ISBN проверяется в конструкторе
            // Status проверяется в конструкторе

            return new Book(id, title, author, isbn, status);
        }

        // ФАБРИЧНЫЙ МЕТОД: для создания НОВОЙ книги
        public static Book New(Title title, Author author, string isbnValue)
        {
            if (title == null) throw new ArgumentNullException(nameof(title));
            if (author == null) throw new ArgumentNullException(nameof(author));

            // ВАЛИДАЦИЯ СТРОКИ ISBN ЧЕРЕЗ РЕГУЛЯРНОЕ ВЫРАЖЕНИЕ (внутри Create)
            var isbn = ISBN.Create(isbnValue);

            var id = BookId.Create(Guid.NewGuid());
            var status = BookStatusValue.Create(BookStatus.Available);

            return Create(id, title, author, isbn, status);
        }


        // Метод для отображения информации о книге
        public override string ToString()
        {
            return $"Книга: {BookTitle} Автор: {Author}, ISBN: {Isbn} - Статус: {Status}";
        }
    }
}