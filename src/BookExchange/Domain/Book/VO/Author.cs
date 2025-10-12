namespace Domain.Book.VO
{
    // Объект значения для Автора книги
    public record Author
    {
        public string Name { get; }

        public Author(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя автора не может быть пустым.", nameof(name));
            }

            Name = name;
        }

        // Переопределяем метод ToString для вывода в виде строки
        public override string ToString() => Name;
    }
}
