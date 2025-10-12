namespace Domain.Book.VO
{
    // Объект значения для Идентификатора книги
    public record BookId
    {
        public Guid Value { get; }

        public BookId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор книги не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        // Переопределяем метод ToString для вывода в виде строки
        public override string ToString() => Value.ToString();
    }
}
