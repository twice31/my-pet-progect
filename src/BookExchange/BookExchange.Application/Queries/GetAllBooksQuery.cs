using BookExchange.Application.Books.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BookExchange.Application.Books.Queries
{
    // IRequest<T> указывает, какой тип данных должен вернуть обработчик
    public record GetAllBooksQuery : IRequest<List<BookDto>>
    {
    }
}