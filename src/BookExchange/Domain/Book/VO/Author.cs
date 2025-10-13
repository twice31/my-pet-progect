namespace Domain.Book.VO
{
    public record Author
    {
        public string Name { get; }

        // Закрытый конструктор
        private Author(string name)
        {
            Name = name;
        }

        // Фабричный метод
        public static Author Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Имя автора не может быть пустым или состоять только из пробелов.", nameof(name));
            }

            return new Author(name);
        }

        public override string ToString() => Name;
    }
}
