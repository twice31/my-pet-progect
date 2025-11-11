using System;

namespace BookExchange.Application.Books.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}