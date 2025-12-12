using System.Collections.Concurrent;
using BookExchange.Presenters.Dtos;

namespace BookExchange.Presenters.Data
{
    /// <summary>
    /// Имитация хранилища данных (in-memory store) для книг.
    /// Теперь реализует IBookStore и НЕ является статическим.
    /// </summary>
    public class InMemoryBookStore : IBookStore
    {
        //Поле _books больше не статическое
        private readonly ConcurrentDictionary<Guid, BookDto> _books = new();

        //Конструктор теперь обычный (не статический)
        public InMemoryBookStore()
        {
            // инициализация тестовыми данными 
            AddBook(new CreateBookDto { Title = "1984", Author = "Джордж Оруэлл", Isbn = "978-0451524935" });
            AddBook(new CreateBookDto { Title = "Мастер и Маргарита", Author = "Михаил Булгаков", Isbn = "978-5-17-046645-1" });
        }

        //У всех методов удален модификатор 'static'

        /// <summary> Добавляет новую книгу, присваивая ей уникальный ID. </summary>
        public BookDto AddBook(CreateBookDto createDto)
        {
            // здесь мы создаем новый BookDto. Статус "Доступна" установлен по умолчанию.
            var newBook = new BookDto
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                Author = createDto.Author,
                Isbn = createDto.Isbn
            };

            _books.TryAdd(newBook.Id, newBook);
            return newBook;
        }

        /// <summary> Возвращает список всех книг. </summary>
        public List<BookDto> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        /// <summary> Возвращает книгу по ID. </summary>
        public BookDto? GetBookById(Guid id)
        {
            return _books.TryGetValue(id, out var book) ? book : null;
        }

        /// <summary> Обновляет существующую книгу. </summary>
        public BookDto? UpdateBook(Guid id, CreateBookDto updateDto)
        {
            if (_books.TryGetValue(id, out var existingBook))
            {
                // Создаем новую версию объекта с обновленными данными, сохраняя старый статус
                var updatedBook = new BookDto
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
        public bool DeleteBook(Guid id)
        {
            // TryRemove возвращает true, если элемент был найден и удален
            return _books.TryRemove(id, out _);
        }
    }
}