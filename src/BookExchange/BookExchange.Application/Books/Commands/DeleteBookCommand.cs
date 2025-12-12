using MediatR;
using System;

namespace BookExchange.Application.Books.Commands
{
    // Команда возвращает bool: true, если удаление прошло успешно, false, если не найдено
    public record DeleteBookCommand : IRequest<bool>
    {
        public Guid Id { get; init; }
    }
}