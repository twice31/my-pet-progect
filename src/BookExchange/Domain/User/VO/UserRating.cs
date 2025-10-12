namespace Domain.User.VO
{
    // Объект значения для рейтинга пользователя
    public record Rating
    {
        public double Value { get; }

        public Rating(double value)
        {
            if (value < 0 || value > 5)
            {
                throw new ArgumentException("Рейтинг должен быть между 0 и 5.", nameof(value));
            }

            Value = value;
        }

        public override string ToString() => $"{Value} stars";
    }

    // Объект значения для отзыва пользователя
    public record Review
    {
        public string Text { get; }
        public DateTime Date { get; }

        public Review(string text, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentException("Отзыв не может быть пустым или состоять только из пробелов.", nameof(text));
            }

            Text = text;
            Date = date;
        }

        public override string ToString() => $"{Text} (Posted on {Date.ToShortDateString()})";
    }

    // Визуализатор рейтинга и отзывов
    public record UserRating
    {
        public Rating Rating { get; }
        public List<Review> Reviews { get; }

        public UserRating(Rating rating, List<Review> reviews)
        {
            Rating = rating;
            Reviews = reviews ?? new List<Review>();
        }

        public override string ToString() => $"Рейтинг: {Rating}, Отзывы: {string.Join(", ", Reviews)}";
    }
}
