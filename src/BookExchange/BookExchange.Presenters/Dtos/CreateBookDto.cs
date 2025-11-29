namespace BookExchange.Presenters.Dtos
{
    /// <summary>
    /// Объект для передачи данных (DTO), используемый для создания или полного обновления книги.
    /// </summary>
    public record CreateBookDto
    {
        /// <summary>Название книги (обязательно).</summary>
        public string Title { get; init; } = string.Empty;

        /// <summary>Автор книги (обязательно).</summary>
        public string Author { get; init; } = string.Empty;

        /// <summary>Международный стандартный книжный номер (необязательно).</summary>
        public string? Isbn { get; init; }
    }
}