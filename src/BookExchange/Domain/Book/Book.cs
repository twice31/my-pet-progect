using Domain.Book.VO;
using Domain.User.VO;
using System;

namespace Domain.Book
{
    public class Book
    {
        public BookId Id { get; }
        public Title Title { get; private set; }
        public Author Author { get; private set; }
        public ISBN Isbn { get; private set; }
        public BookStatus Status { get; private set; }
        public UserId OwnerId { get; }

        private Book(BookId id, Title title, Author author, ISBN isbn, BookStatus status, UserId ownerId)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title), "Название книги не может быть null.");
            Author = author ?? throw new ArgumentNullException(nameof(author), "Автор книги не может быть null.");
            Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn), "ISBN книги не может быть null.");
            Status = status ?? throw new ArgumentNullException(nameof(status), "Статус книги не может быть null.");
            OwnerId = ownerId ?? throw new ArgumentNullException(nameof(ownerId), "Владелец книги не может быть null.");
        }

        public static Book Create(BookId id, Title title, Author author, ISBN isbn, BookStatus status, UserId ownerId)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Идентификатор книги не может быть пустым.");
            if (title == null) throw new ArgumentNullException(nameof(title), "Название книги не может быть пустым.");
            if (author == null) throw new ArgumentNullException(nameof(author), "Автор книги не может быть пустым.");
            if (ownerId == null) throw new ArgumentNullException(nameof(ownerId), "Владелец книги не может быть пустым.");

            return new Book(id, title, author, isbn, status, ownerId);
        }

        public static Book New(Title title, Author author, ISBN isbn, UserId ownerId)
        {
            var id = BookId.Create(Guid.NewGuid());
            var status = BookStatus.Available;

            return Create(id, title, author, isbn, status, ownerId);
        }

        /// <summary>
        /// Обновляет детали книги (Название, Автор, ISBN).
        /// </summary>
        /// <param name="newTitle">Новое название книги (VO).</param>
        /// <param name="newAuthor">Новый автор книги (VO).</param>
        /// <param name="newIsbn">Новый ISBN книги (VO).</param>
        public void UpdateDetails(Title newTitle, Author newAuthor, ISBN newIsbn)
        {
            Title = newTitle ?? throw new ArgumentNullException(nameof(newTitle), "Название книги не может быть null при обновлении.");
            Author = newAuthor ?? throw new ArgumentNullException(nameof(newAuthor), "Автор книги не может быть null при обновлении.");
            Isbn = newIsbn ?? throw new ArgumentNullException(nameof(newIsbn), "ISBN книги не может быть null при обновлении.");
        }

        public void UpdateStatus(BookStatus newStatus)
        {
            if (Status == newStatus)
            {
                throw new InvalidOperationException("Статус книги уже установлен на выбранное значение.");
            }


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

        public override string ToString()
        {
            return $"Книга: {Title} Автор: {Author}, ISBN: {Isbn} - Статус: {Status.Name}";
        }
    }
}