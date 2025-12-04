using BookExchange.Presenters.Data;
using BookExchange.Presenters.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookExchange.Presenters.Controllers
{
    /// <summary>
    /// Контроллер для управления книгами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {

        /// <summary>
        /// Возвращает список всех книг в системе.
        /// </summary>
        /// <returns>Контейнер Envelope, содержащий список BookDto.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Envelope.Envelope<List<BookDto>>), StatusCodes.Status200OK)]
        public IResult GetAll() 
        {
            var books = InMemoryBookStore.GetAllBooks(); 

            return Envelope.Envelope<List<BookDto>>.Ok(books);
        }

        /// <summary>
        /// Возвращает конкретную книгу по ее уникальному идентификатору (ID).
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid).</param>
        /// <returns>Контейнер Envelope, содержащий BookDto или ошибку, если книга не найдена.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Envelope.Envelope<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status404NotFound)] 
        public IResult GetById(Guid id) 
        {
            var book = InMemoryBookStore.GetBookById(id); 

            if (book == null)
            {
                return Envelope.Envelope<BookDto>.NotFound($"Книга с ID '{id}' не найдена.");
            }

            return Envelope.Envelope<BookDto>.Ok(book);
        }

        /// <summary>
        /// Создает новую книгу в системе обмена.
        /// </summary>
        /// <param name="dto">Данные для создания книги (Title, Author, Isbn).</param>
        /// <returns>Контейнер Envelope, содержащий созданную BookDto.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Envelope.Envelope<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status400BadRequest)] 
        public IResult Create([FromBody] CreateBookDto dto) 
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Author))
            {
                return Envelope.Envelope<BookDto>.BadRequest("Название и автор книги обязательны.");
            }

            var newBook = InMemoryBookStore.AddBook(dto);

            return Envelope.Envelope<BookDto>.Created(newBook);
        }

        /// <summary>
        /// Полностью обновляет существующую книгу.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid) для обновления.</param>
        /// <param name="dto">Новые данные для книги (используется CreateBookDto).</param>
        /// <returns>Контейнер Envelope, содержащий обновленную BookDto.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Envelope.Envelope<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status400BadRequest)] 
        public IResult Update(Guid id, [FromBody] CreateBookDto dto) 
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Author))
            {
                return Envelope.Envelope<BookDto>.BadRequest("Название и автор книги обязательны для обновления.");
            }

            var updatedBook = InMemoryBookStore.UpdateBook(id, dto); 

            if (updatedBook == null)
            {
                return Envelope.Envelope<BookDto>.NotFound($"Невозможно обновить. Книга с ID '{id}' не найдена.");
            }

            return Envelope.Envelope<BookDto>.Ok(updatedBook);
        }

        /// <summary>
        /// Удаляет книгу из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid) для удаления.</param>
        /// <returns>Ответ без контента (204 No Content) или ошибка 404.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status404NotFound)] 
        public IResult Delete(Guid id) 
        {
            var isDeleted = InMemoryBookStore.DeleteBook(id); 

            if (!isDeleted)
            {
                return Envelope.Envelope.NotFound($"Невозможно удалить. Книга с ID '{id}' не найдена.");
            }

            return Envelope.Envelope.NoContent();
        }
    }
}