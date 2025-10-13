namespace Domain.User.VO
{
    // Объект значения для Идентификатора пользователя
    public record UserId
    {
        public Guid Value { get; }

        // Закрытый конструктор
        private UserId(Guid value)
        {
            Value = value;
        }

        // Фабричный метод для создания объекта
        public static UserId Create(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Идентификатор пользователя не может быть пустым.", nameof(value));
            }

            return new UserId(value);
        }

        public override string ToString() => Value.ToString();
    }
}
