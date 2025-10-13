namespace Domain.User.VO
{
    // Объект значения для рейтинга пользователя
    public record Rating
    {
        public const double MaxValueAmount = 5.0; // Константа для максимального значения

        public double Value { get; }

        // ЗАКРЫТЫЙ КОНСТРУКТОР: используется только внутри класса для инициализации.
        private Rating(double value)
        {
            Value = value;
        }

        // ФАБРИЧНЫЙ СТАТИЧЕСКИЙ МЕТОД: выполняет валидацию и создает объект
        public static Rating Create(double ratingValue)
        {
            if (ratingValue < 0)
                throw new ArgumentException("Рейтинг не может быть отрицательным", nameof(ratingValue));

            if (ratingValue > MaxValueAmount)
                throw new ArgumentException($"Рейтинг не может превышать его максимальное значение: {MaxValueAmount}", nameof(ratingValue));

            return new Rating(ratingValue);
        }

        public override string ToString() => $"{Value} звёзд";
    }
}