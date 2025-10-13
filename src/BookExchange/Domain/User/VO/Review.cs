namespace Domain.User.VO
{
    // Объект значения для отзыва пользователя
    public record Review
    {
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

            return new Review(text, date);
        }

        public override string ToString() => $"{Text} (Опубликовано {Date.ToShortDateString()})";
    }
}
