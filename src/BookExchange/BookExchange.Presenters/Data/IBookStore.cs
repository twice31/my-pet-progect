using BookExchange.Presenters.Dtos;

namespace BookExchange.Presenters.Data
{
    /// <summary>
    /// Интерфейс для сервиса управления книгами.
    /// Это и есть наша абстракция, которую мы будем внедрять (DI).
    /// </summary>
    public interface IBookStore
    {
        // CRUD-операции (Create, Read, Update, Delete)

        /// <summary> Добавляет новую книгу. </summary>
        BookDto AddBook(CreateBookDto createDto);

        /// <summary> Возвращает список всех книг. </summary>
        List<BookDto> GetAllBooks();

        /// <summary> Возвращает книгу по ID. </summary>
        BookDto? GetBookById(Guid id);

        /// <summary> Обновляет существующую книгу. </summary>
        BookDto? UpdateBook(Guid id, CreateBookDto updateDto);

        /// <summary> Удаляет книгу по ID. </summary>
        bool DeleteBook(Guid id);
    }
}