namespace Domain.Book.VO
{
    // Объект значения для Названия книги
    public record Title
    {
        public string Value { get; }

        public Title(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Название книги не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        // Переопределяем метод ToString для вывода в виде строки
        public override string ToString() => Value;
    }
}
