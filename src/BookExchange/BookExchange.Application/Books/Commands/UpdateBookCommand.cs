using BookExchange.Application.Books.DTOs;
using MediatR;
using System;

namespace BookExchange.Application.Books.Commands
{
    // Команда возвращает обновленный BookDto или null
    public record UpdateBookCommand : IRequest<BookDto?>
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public string ISBN { get; init; } = string.Empty;
    }
}