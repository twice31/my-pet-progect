using BookExchange.Presenters.Dtos;

namespace BookExchange.Presenters.Services
{
    /// <summary>
    /// Интерфейс для сервиса управления книгами.
    /// Определяет контракт для всех операций CRUD,
    /// позволяя легко заменить реализацию хранилища данных.
    /// </summary>
    public interface IBookService
    {
        List<BookDto> GetAllBooks();
        BookDto? GetBookById(Guid id);
        BookDto AddBook(CreateBookDto dto);
        BookDto? UpdateBook(Guid id, CreateBookDto dto);
        bool DeleteBook(Guid id);
    }
}