using BookExchange.Application.Books.Commands;
using BookExchange.Application.Books.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace BookExchange.Presenters.Controllers
{
    /// <summary>
    /// Контроллер для управления операциями, связанными с книгами.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создает новую книгу в системе.
        /// </summary>
        /// <param name="command">Данные книги и ID владельца.</param>
        /// <returns>Возвращает статус 201 Created и DTO созданной книги.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command)
        {
            var ownerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(ownerIdClaim) || !Guid.TryParse(ownerIdClaim, out var ownerIdGuid))
            {
                return Unauthorized("Не удалось определить идентификатор владельца книги. Требуется аутентификация.");
            }

            command.OwnerId = ownerIdGuid;

            var bookDto = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, bookDto);
        }
    }
}