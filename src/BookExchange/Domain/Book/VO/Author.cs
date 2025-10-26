using System;

namespace Domain.Book.VO
{
    public record Author
    {
        // Добавляем константу максимальной длины
        public const int MAX_LENGTH = 150;

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
            // Проверка на максимальную длину
            if (name.Length > MAX_LENGTH)
            {
                throw new ArgumentException($"Имя автора не может превышать {MAX_LENGTH} символов.", nameof(name));
            }

            return new Author(name);
        }

        public override string ToString() => Name;
    }
}