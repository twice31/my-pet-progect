namespace Domain.Book.VO
{
    public record Title
    {
        public string Value { get; }

        // Закрытый конструктор
        private Title(string value)
        {
            Value = value;
        }

        // Фабричный метод
        public static Title Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название книги не может быть пустым или состоять только из пробелов.", nameof(value));
            }

            return new Title(value);
        }

        public override string ToString() => Value;
    }
}
