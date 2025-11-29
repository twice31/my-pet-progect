using System.Collections.Concurrent;
using BookExchange.Presenters.Dtos; 

namespace BookExchange.Presenters.Data
{
    /// <summary>
    /// Имитация хранилища данных (in-memory store) для книг, использующая статический словарь.
    /// хранилище в виде статического словаря.
    /// </summary>
    public static class InMemoryBookStore
    {
        private static readonly ConcurrentDictionary<Guid, BookExchange.Presenters.Dtos.BookDto> _books = new();

        static InMemoryBookStore()
        {
            // инициалиазция тестовыми данными 
            AddBook(new BookExchange.Presenters.Dtos.CreateBookDto { Title = "1984", Author = "Джордж Оруэлл", Isbn = "978-0451524935" });
            AddBook(new BookExchange.Presenters.Dtos.CreateBookDto { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Isbn = "978-5-17-046645-1" });
        }

        /// <summary> Добавляет новую книгу, присваивая ей уникальный ID. </summary>
        public static BookExchange.Presenters.Dtos.BookDto AddBook(BookExchange.Presenters.Dtos.CreateBookDto createDto)
        {
            // здесь мы создаем новый BookDto. Статус "Доступна" установлен по умолчанию.
            var newBook = new BookExchange.Presenters.Dtos.BookDto
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                Author = createDto.Author,
                Isbn = createDto.Isbn,
                Status = "Доступна"
            };

            _books.TryAdd(newBook.Id, newBook);
            return newBook;
        }

        /// <summary> Возвращает список всех книг. </summary>
        public static List<BookExchange.Presenters.Dtos.BookDto> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        /// <summary> Возвращает книгу по ID. </summary>
        public static BookExchange.Presenters.Dtos.BookDto? GetBookById(Guid id)
        {
            return _books.TryGetValue(id, out var book) ? book : null;
        }

        /// <summary> Обновляет существующую книгу. </summary>
        public static BookExchange.Presenters.Dtos.BookDto? UpdateBook(Guid id, BookExchange.Presenters.Dtos.CreateBookDto updateDto)
        {
            if (_books.TryGetValue(id, out var existingBook))
            {
                // Создаем новую версию объекта с обновленными данными, сохраняя старый статус
                var updatedBook = new BookExchange.Presenters.Dtos.BookDto
                {
                    Id = id,
                    Title = updateDto.Title,
                    Author = updateDto.Author,
                    Isbn = updateDto.Isbn,
                    Status = existingBook.Status
                };

                // Используем TryUpdate для атомарного обновления
                _books.TryUpdate(id, updatedBook, existingBook);
                return updatedBook;
            }
            return null;
        }

        /// <summary> Удаляет книгу по ID. </summary>
        public static bool DeleteBook(Guid id)
        {
            // TryRemove возвращает true, если элемент был найден и удален
            return _books.TryRemove(id, out _);
        }
    }
}