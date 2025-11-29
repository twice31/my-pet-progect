using BookExchange.Presenters.Data;
using BookExchange.Presenters.Dtos;
using BookExchange.Presenters.Envelope;
using Microsoft.AspNetCore.Mvc;

namespace BookExchange.Presenters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        /// <summary>
        /// Возвращает список всех книг в системе.
        /// </summary>
        /// <returns>Контейнер Envelope, содержащий список BookDto.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(Envelope<List<BookDto>>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var books = InMemoryBookStore.GetAllBooks();
            return Ok(new Envelope<List<BookDto>>(books));
        }


        /// <summary>
        /// Возвращает конкретную книгу по ее уникальному идентификатору (ID).
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid).</param>
        /// <returns>Контейнер Envelope, содержащий BookDto или ошибку, если книга не найдена.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var book = InMemoryBookStore.GetBookById(id);

            if (book == null)
            {
                return NotFound(new Envelope<BookDto>($"Книга с ID '{id}' не найдена."));
            }

            return Ok(new Envelope<BookDto>(book));
        }


        /// <summary>
        /// Создает новую книгу в системе обмена.
        /// </summary>
        /// <param name="dto">Данные для создания книги (Title, Author, Isbn).</param>
        /// <returns>Контейнер Envelope, содержащий созданную BookDto.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateBookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Author))
            {
                return BadRequest(new Envelope<BookDto>("Название и автор книги обязательны."));
            }

            var newBook = InMemoryBookStore.AddBook(dto);

            // Возвращаем 201 Created и ссылку на созданный ресурс
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, new Envelope<BookDto>(newBook));
        }


        /// <summary>
        /// Полностью обновляет существующую книгу.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid) для обновления.</param>
        /// <param name="dto">Новые данные для книги.</param>
        /// <returns>Контейнер Envelope, содержащий обновленную BookDto.</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Envelope<BookDto>), StatusCodes.Status400BadRequest)]
        public IActionResult Update(Guid id, [FromBody] CreateBookDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Author))
            {
                return BadRequest(new Envelope<BookDto>("Название и автор книги обязательны для обновления."));
            }

            var updatedBook = InMemoryBookStore.UpdateBook(id, dto);

            if (updatedBook == null)
            {
                return NotFound(new Envelope<BookDto>($"Невозможно обновить. Книга с ID '{id}' не найдена."));
            }

            return Ok(new Envelope<BookDto>(updatedBook));
        }


        /// <summary>
        /// Удаляет книгу из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор книги (Guid) для удаления.</param>
        /// <returns>Ответ без контента (204 No Content) или ошибка 404.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Envelope<object>), StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var isDeleted = InMemoryBookStore.DeleteBook(id);

            if (!isDeleted)
            {
                return NotFound(new Envelope<object>($"Невозможно удалить. Книга с ID '{id}' не найдена."));
            }

            return NoContent();
        }
    }
}