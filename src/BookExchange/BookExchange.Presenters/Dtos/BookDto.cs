namespace BookExchange.Presenters.Dtos
{
    /// <summary>
    /// Объект для передачи данных (DTO), представляющий книгу, возвращаемую клиенту.
    /// </summary>
    public record BookDto
    {
        /// <summary>Уникальный идентификатор книги.</summary>
        public Guid Id { get; init; }

        /// <summary>Название книги.</summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>Автор книги.</summary>
        public string Author { get; init; } = string.Empty;

        /// <summary>Международный стандартный книжный номер (ISBN).</summary>
        public string? Isbn { get; init; }

        /// <summary>Статус книги в системе обмена.</summary>
        public string Status { get; init; } = "Доступна";
    }
}