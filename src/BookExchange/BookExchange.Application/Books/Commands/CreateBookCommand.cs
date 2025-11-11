using BookExchange.Application.Books.DTOs;
using MediatR;
using System;

namespace BookExchange.Application.Books.Commands
{
    public class CreateBookCommand : IRequest<BookDto>
    {
        public Guid OwnerId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
    }
}