using BookExchange.Application.Books.DTOs;
using MediatR;
using System;

namespace BookExchange.Application.Books.Queries
{
    // Запрос, который вернет BookDto или null
    public record GetBookByIdQuery : IRequest<BookDto?>
    {
        public Guid BookId { get; init; }
    }
}