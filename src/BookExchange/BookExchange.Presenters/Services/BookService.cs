using BookExchange.Presenters.Dtos;
using BookExchange.Presenters.Data;
using BookExchange.Presenters.Services; 

namespace BookExchange.Presenters.Services
{

    /// <summary>
    /// Сервис для управления книгами, использующий конструкторную инъекцию.
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookStore _store;

        public BookService(IBookStore store)
        {
            _store = store;
        }

        /// <inheritdoc />
        public List<BookDto> GetAllBooks()
        {
            return _store.GetAllBooks();
        }

        /// <inheritdoc />
        public BookDto? GetBookById(Guid id)
        {
            return _store.GetBookById(id);
        }

        /// <inheritdoc />
        public BookDto AddBook(CreateBookDto dto)
        {
            return _store.AddBook(dto);
        }

        /// <inheritdoc />
        public BookDto? UpdateBook(Guid id, CreateBookDto dto)
        {
            return _store.UpdateBook(id, dto);
        }

        /// <inheritdoc />
        public bool DeleteBook(Guid id)
        {
            return _store.DeleteBook(id);
        }
    }
}