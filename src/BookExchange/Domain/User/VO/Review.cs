using System;

namespace Domain.User.VO
{
    // Объект значения для отзыва пользователя
    public record Review
    {
        // Добавляем константу максимальной длины
        public const int MAX_LENGTH = 500;

        public string Text { get; }
        public DateTime Date { get; }

        // Закрытый конструктор
        private Review(string text, DateTime date)
        {
            Text = text;
            Date = date;
        }

        // Фабричный метод для создания объекта
        public static Review Create(string text, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Отзыв не может быть пустым или состоять только из пробелов.", nameof(text));
            }
            // Проверка на максимальную длину
            if (text.Length > MAX_LENGTH)
            {
                throw new ArgumentException($"Отзыв не может превышать {MAX_LENGTH} символов.", nameof(text));
            }

            return new Review(text, date);
        }

        public override string ToString() => $"{Text} (Опубликовано {Date.ToShortDateString()})";
    }
}