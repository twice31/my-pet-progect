namespace Domain.User.VO
{
    // Объект значения для Идентификатора пользователя
    public record UserId
    {
        public Guid Value { get; }

        public UserId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор пользователя не может быть пустым.", nameof(value));
            }

            Value = value;
        }

        // Переопределяем метод ToString для вывода в виде строки
        public override string ToString() => Value.ToString();
    }
}
