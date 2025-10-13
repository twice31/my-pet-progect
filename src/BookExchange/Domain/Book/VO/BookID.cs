namespace Domain.Book.VO
{
    public record BookId
    {
        public Guid Value { get; }

        // Закрытый конструктор
        private BookId(Guid value)
        {
            Value = value;
        }

        // Фабричный метод
        public static BookId Create(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор книги не может быть пустым.", nameof(value));
            }

            return new BookId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
