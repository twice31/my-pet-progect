using Microsoft.AspNetCore.Mvc;
using MediatR;
using BookExchange.Application.Books.DTOs;
using BookExchange.Application.Books.Commands;
using BookExchange.Application.Books.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;
using CreateBookDto = BookExchange.Presenters.Dtos.CreateBookDto;

namespace BookExchange.Presenters.Controllers
{
    /// <summary>
    /// Контроллер для управления книгами.
    /// Перенаправляет запросы в Application-слой через IMediator (CQRS).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Возвращает список всех книг в системе (CQRS Query: GetAllBooksQuery).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Envelope.Envelope<List<BookDto>>), StatusCodes.Status200OK)]
        public async Task<IResult> GetAll(CancellationToken ct)
        {
            var query = new GetAllBooksQuery();
            List<BookDto> books = await _mediator.Send(query, ct);
            return Envelope.Envelope<List<BookDto>>.Ok(books);
        }

        /// <summary>
        /// Создает новую книгу (CQRS Command: CreateBookCommand).
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Envelope.Envelope<BookDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Envelope.Envelope), StatusCodes.Status400BadRequest)]
        public async Task<IResult> Create([FromBody] CreateBookDto dto, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(dto.Title) || string.IsNullOrWhiteSpace(dto.Author))
            {
                return Envelope.Envelope<BookDto>.BadRequest("Название и автор книги обязательны.");
            }

            var command = new CreateBookCommand
            {
                OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = dto.Title,
                Author = dto.Author,
                ISBN = dto.Isbn ?? string.Empty
            };

            try
            {
                BookDto newBookDto = await _mediator.Send(command, ct);
                return Envelope.Envelope<BookDto>.Created(newBookDto);
            }
            catch (Exception ex)
            {
                return Envelope.Envelope<BookDto>.BadRequest(ex.Message);
            }
        }

    }
}