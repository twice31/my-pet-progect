using System;

namespace Domain.Book.VO
{
    public record Title
    {
        // Добавляем константу максимальной длины
        public const int MAX_LENGTH = 250;

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
            // Проверка на максимальную длину
            if (value.Length > MAX_LENGTH)
            {
                throw new ArgumentException($"Название книги не может превышать {MAX_LENGTH} символов.", nameof(value));
            }

            return new Title(value);
        }

        public override string ToString() => Value;
    }
}